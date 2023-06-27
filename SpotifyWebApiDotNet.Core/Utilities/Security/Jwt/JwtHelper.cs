using IdentityServer3.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SpotifyWebApiDotNet.Core.Entities.Concrete;
using SpotifyWebApiDotNet.Core.Extensions;
using SpotifyWebApiDotNet.Core.Utilities.Security.Enycption;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SpotifyWebApiDotNet.Core.Utilities.Security.Jwt
{
	public class JwtHelper : ITokenHelper
	{
		public IConfiguration Configuration { get; }
		private TokenOptions _tokenOptions;
		private DateTime _accessTokenExpiration;

		public JwtHelper(IConfiguration configuration)
		{
			Configuration = configuration;
			_tokenOptions = Configuration.GetSection(key: "TokenOptions").Get<TokenOptions>();
		}

		public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
		{
			_accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
			var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
			var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
			var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
			var jwtSecurutyTokenHandler = new JwtSecurityTokenHandler();
			var token = jwtSecurutyTokenHandler.WriteToken(jwt);

			return new AccessToken
			{
				Token = token,
				Expiration = _accessTokenExpiration
			};
		}

		public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
		{
			var jwt = new JwtSecurityToken(
				issuer: tokenOptions.Isuser,
				audience: tokenOptions.Audience,
				expires: _accessTokenExpiration,
				notBefore: DateTime.Now,
				claims: SetClaims(user, operationClaims),
				signingCredentials: signingCredentials);

			return jwt;
		}

		private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
		{
			var claims = new List<Claim>();
			claims.AddNameIdentifier(user.Id.ToString());
			claims.AddEmail(user.Email);
			claims.AddName($"{user.FirstName} {user.LastName}");
			claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());
			return claims;
		}

		public int GetAuthenticatedUser(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_tokenOptions.SecurityKey);
			tokenHandler.ValidateToken(token, new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = false,
				ValidateAudience = false,
				ClockSkew = TimeSpan.Zero
			}, out SecurityToken validatedToken);

			var jwtToken = (JwtSecurityToken)validatedToken;
			var userId = int.Parse(jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
			return userId;
		}

		public bool ValidateToken(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_tokenOptions.SecurityKey);
			var validationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(key),
				ValidateIssuer = true,
				ValidIssuer = _tokenOptions.Isuser,
				ValidateAudience = true,
				ValidAudience = _tokenOptions.Audience,
				ClockSkew = TimeSpan.Zero
			};

			SecurityToken validatedToken;
			var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
			return true;
		}
	}
}

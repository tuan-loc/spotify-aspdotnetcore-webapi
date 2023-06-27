using Microsoft.IdentityModel.Tokens;

namespace SpotifyWebApiDotNet.Core.Utilities.Security.Enycption
{
	public class SigningCredentialsHelper
	{
		public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
		{
			return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
		}
	}
}

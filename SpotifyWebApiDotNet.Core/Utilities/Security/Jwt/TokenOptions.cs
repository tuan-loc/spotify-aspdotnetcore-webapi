namespace SpotifyWebApiDotNet.Core.Utilities.Security.Jwt
{
	public class TokenOptions
	{
		public string Audience { get; set; }
		public string Isuser { get; set; }
		public int AccessTokenExpiration { get; set; }
		public string SecurityKey { get; set; }
	}
}

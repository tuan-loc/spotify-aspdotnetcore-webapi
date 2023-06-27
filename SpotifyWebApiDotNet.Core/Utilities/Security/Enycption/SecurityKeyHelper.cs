using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SpotifyWebApiDotNet.Core.Utilities.Security.Enycption
{
	public class SecurityKeyHelper
	{
		public static SecurityKey CreateSecurityKey(string securityKey)
		{
			return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
		}
	}
}

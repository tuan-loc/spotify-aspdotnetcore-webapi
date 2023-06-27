using SpotifyWebApiDotNet.Core.Entities.Concrete;

namespace SpotifyWebApiDotNet.Core.Utilities.Security.Jwt
{
	public interface ITokenHelper
	{
		AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
		int GetAuthenticatedUser(string token);
		bool ValidateToken(string token);
	}
}

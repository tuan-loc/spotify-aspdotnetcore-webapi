using SpotifyWebApiDotNet.Core.Entities.Concrete.Base;

namespace SpotifyWebApiDotNet.Core.Entities.Concrete
{
	public class UserOperationClaim : BaseEntity, IEntity
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int OperationClaimId { get; set; }
	}
}

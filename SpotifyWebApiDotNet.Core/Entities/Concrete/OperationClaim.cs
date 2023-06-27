using SpotifyWebApiDotNet.Core.Entities.Concrete.Base;

namespace SpotifyWebApiDotNet.Core.Entities.Concrete
{
	public class OperationClaim : BaseEntity, IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}

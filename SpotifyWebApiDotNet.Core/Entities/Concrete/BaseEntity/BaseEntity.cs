namespace SpotifyWebApiDotNet.Core.Entities.Concrete.Base
{
	public class BaseEntity
	{
		public bool Status { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public DateTime ModifiedDate { get; set; } = DateTime.Now;
	}
}

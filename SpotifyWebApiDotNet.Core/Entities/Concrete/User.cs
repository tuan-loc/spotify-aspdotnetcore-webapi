﻿using SpotifyWebApiDotNet.Core.Entities.Concrete.Base;

namespace SpotifyWebApiDotNet.Core.Entities.Concrete
{
	public class User : BaseEntity, IEntity
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public byte[] PasswordSalt { get; set; }
		public byte[] PasswordHash { get; set; }
	}
}

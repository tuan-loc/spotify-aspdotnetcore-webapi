﻿using System.Text;

namespace SpotifyWebApiDotNet.Core.Utilities.Security.Hashing
{
	public class HashingHelper
	{
		public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using(var hmac = new System.Security.Cryptography.HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(buffer: Encoding.UTF8.GetBytes(password));
			}
		}

		public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
			{
				var computeHash = hmac.ComputeHash(buffer: Encoding.UTF8.GetBytes(password));
				for (int i = 0; i < computeHash.Length; i++)
				{
					if (computeHash[i] != passwordHash[i])
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.LoginSecurity.Helper
{
	public static class HashingHelper
	{
		public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using var hmac = new HMACSHA512();
			passwordSalt = hmac.Key;
			passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
		}

		public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using var hmac = new HMACSHA512(passwordSalt);
			var computedPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
			return !computedPasswordHash.Where((t, i) => t != passwordHash[i]).Any();
		}
	}
}

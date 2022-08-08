using Identity.Core.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Core.Services
{
	public class HashingService : IHashingService
	{
		public (byte[] hash, byte[] salt) CreateHash(string value)
		{
			byte[] hash, salt;
			using(var hmac = new HMACSHA512())
			{
				salt = hmac.Key;
				hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(value));
			}

			return (hash, salt);
		}

		public bool ValidateHash(string value, byte[] hash, byte[] salt)
		{
			using (var hmac = new HMACSHA512(salt))
			{
				byte[] hashedValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(value));
				return hashedValue.SequenceEqual(hash);
			}
		}
	}
}

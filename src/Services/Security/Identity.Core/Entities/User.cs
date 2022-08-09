using Identity.Core.Entities;

namespace Identity.Core.Entities
{
	public class User
	{
		public string UserName { get; set; }

		public byte[] PasswordHash { get; set; }

		public byte[] PasswordSalt { get; set; }

		public Role UserRole { get; set; }
	}
}

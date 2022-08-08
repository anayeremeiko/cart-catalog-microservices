namespace Identity.Infrastructure.Entities
{
	public class UserInformation
	{
		public string UserName { get; set; }

		public byte[] PasswordHash { get; set; }

		public byte[] PasswordSalt { get; set; }

		public int UserRoleId { get; set; }

		public virtual UserRole UserRole { get; set; }
	}
}

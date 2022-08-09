namespace Identity.Infrastructure.Entities
{
	public class UserRole
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public bool CanRead { get; set; }

		public bool CanEdit { get; set; }

		public bool CanCreate { get; set; }

		public bool CanDelete { get; set; }

		public virtual List<UserInformation> Users { get; set; }
	}
}

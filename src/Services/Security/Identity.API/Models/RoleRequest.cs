namespace Identity.API.Models
{
	public class RoleRequest
	{
		public string Name { get; set; }

		public bool CanRead { get; set; }

		public bool CanEdit { get; set; }

		public bool CanCreate { get; set; }

		public bool CanDelete { get; set; }
	}
}

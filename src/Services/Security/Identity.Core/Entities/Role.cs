namespace Identity.Core.Entities
{
	public class Role
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public bool CanRead { get; set; }

		public bool CanEdit { get; set; }

		public bool CanCreate { get; set; }

		public bool CanDelete { get; set; }
	}
}

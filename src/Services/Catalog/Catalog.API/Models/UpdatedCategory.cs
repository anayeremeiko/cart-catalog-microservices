namespace Catalog.API.Models
{
	public class UpdatedCategory
	{
		public string Name { get; set; }

		public string? ImageUrl { get; set; }

		public int? ParentCategoryId { get; set; }
	}
}

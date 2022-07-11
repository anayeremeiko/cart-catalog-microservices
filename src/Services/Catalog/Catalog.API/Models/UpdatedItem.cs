namespace Catalog.API.Models
{
	public class UpdatedItem
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public string ImageUrl { get; set; }

		public int CategoryId { get; set; }

		public decimal Price { get; set; }

		public int Amount { get; set; }
	}
}

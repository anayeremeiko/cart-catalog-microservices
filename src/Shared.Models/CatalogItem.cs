namespace Shared.Models
{
	public class CatalogItem
	{
		/// <summary>
		/// The identifier of the product.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The name of the product.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The URL of the product image. Optional.
		/// </summary>
		public string ImageUrl { get; set; }

		/// <summary>
		/// The alternative text of the product image. Optional.
		/// </summary>
		public string ImageAlt { get; set; }

		/// <summary>
		/// The price of the product.
		/// </summary>
		public decimal Price { get; set; }
	}
}
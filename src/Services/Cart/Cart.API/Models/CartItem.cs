using System.ComponentModel.DataAnnotations;

namespace eShopServices.Services.Cart.Cart.API.Models
{
	/// <summary>
	/// A model for cart item.
	/// </summary>
	public class CartItem
	{
		/// <summary>
		/// The identifier of the product.
		/// </summary>
		[Required]
		public int Id { get; set; }

		/// <summary>
		/// The name of the product.
		/// </summary>
		[Required]
		public string Name { get; set; }

		/// <summary>
		/// The URL of the product image. Optional.
		/// </summary>
		[Url]
		public string ImageUrl { get; set; }

		/// <summary>
		/// The alternative text of the product image. Optional.
		/// </summary>
		public string ImageAlt { get; set; }

		/// <summary>
		/// The price of the product.
		/// </summary>
		[Required]
		public decimal Price { get; set; }

		/// <summary>
		/// The quantity of the product in the cart.
		/// </summary>
		[Range(1, int.MaxValue)]
		public int Quantity { get; set; }

		/// <summary>
		/// Increases quantity of the item in the cart.
		/// </summary>
		/// <param name="quantity">The added quantity.</param>
		public void AddQuantity(int quantity)
		{
			if (quantity > 0) 
			{ 
				this.Quantity += quantity;
			}
		}

		/// <summary>
		/// Decreases quantity of the item in the cart by 1.
		/// </summary>
		public void RemoveQuantity(int quantity)
		{
			if (quantity < 0) return;

			if (this.Quantity - quantity >= 0)
			{
				this.Quantity -= quantity;
			} else
			{
				this.Quantity = 0;
			}
		}
	}
}

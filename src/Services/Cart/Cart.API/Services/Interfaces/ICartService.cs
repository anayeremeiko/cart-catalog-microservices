using eShopServices.Services.Cart.Cart.API.Models;

namespace eShopServices.Services.Cart.Cart.API.Services.Interfaces
{
	/// <summary>
	/// An interface for cart service.
	/// </summary>
	public interface ICartService
	{
		/// <summary>
		/// Get all items that cart with provided id contains.
		/// </summary>
		/// <param name="cartId">The id of the cart with the items.</param>
		/// <returns>All items contained in the cart.</returns>
		IEnumerable<CartItem> GetAllCartItems(string cartId);

		/// <summary>
		/// Adds item to the cart with the provided id.
		/// </summary>
		/// <param name="cartId">The id of the cart to add item to.</param>
		/// <param name="item">The item to add to the cart.</param>
		/// <returns>The updated cart.</returns>
		Models.Cart AddItemToCart(string cartId, CartItem item);

		/// <summary>
		/// Removes item from the cart with the provided id. 
		/// If the quantity of the item will be less than 1 after the end of the operation, 
		/// the item will be completely removed from the cart.
		/// </summary>
		/// <param name="cartId">The id of the cart to remove the item from.</param>
		/// <param name="itemId">The id of the item to remove.</param>
		/// <returns>The updated cart.</returns>
		Models.Cart RemoveItemFromCart(string cartId, int itemId);

		void UpdateItemInCarts(CartItem item);
	}
}

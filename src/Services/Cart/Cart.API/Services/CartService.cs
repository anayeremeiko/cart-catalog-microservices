using eShopServices.Services.Cart.Cart.API.DataServices.Interfaces;
using eShopServices.Services.Cart.Cart.API.Models;
using eShopServices.Services.Cart.Cart.API.Services.Interfaces;

namespace eShopServices.Services.Cart.Cart.API.Services
{
	public class CartService : ICartService
	{
		private readonly IDataService<Models.Cart> dataService;

		public CartService(IDataService<Models.Cart> dataService)
		{
			this.dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
		}

		/// <summary>
		/// Adds item to the cart with the provided id.
		/// </summary>
		/// <param name="cartId">The id of the cart to add item to.</param>
		/// <param name="item">The item to add to the cart.</param>
		/// <returns>The updated cart.</returns>
		public Models.Cart AddItemToCart(string cartId, CartItem item)
		{
			Models.Cart cart = this.dataService.GetById(cartId);

			if (cart == null)
			{
				cart = new Models.Cart(cartId);
				cart.AddItem(item);
				this.dataService.Add(cart);
			}
			else
			{
				cart.AddItem(item);
				this.dataService.Update(cart);
			}

			return cart;

		}

		/// <summary>
		/// Get all items that cart with provided id contains.
		/// </summary>
		/// <param name="cartId">The id of the cart with the items.</param>
		/// <returns>All items contained in the cart.</returns>
		public IEnumerable<CartItem> GetAllCartItems(string cartId)
		{
			Models.Cart cart = this.dataService.GetById(cartId);

			if (cart == null)
			{
				return Enumerable.Empty<CartItem>();
			}

			return cart.Items.AsEnumerable();
		}

		/// <summary>
		/// Removes item from the cart with the provided id. 
		/// If the quantity of the item will be less than 1 after the end of the operation, 
		/// the item will be completely removed from the cart.
		/// </summary>
		/// <param name="cartId">The id of the cart to remove the item from.</param>
		/// <param name="itemId">The id of the item to remove.</param>
		/// <returns>The updated cart.</returns>
		public Models.Cart RemoveItemFromCart(string cartId, int itemId)
		{
			Models.Cart cart = this.dataService.GetById(cartId);

			if (cart == null)
			{
				throw new InvalidOperationException($"Cart with id {cartId} does not contain item with {itemId} id.");
			}

			cart.RemoveItem(itemId);
			this.dataService.Update(cart);

			return cart;
		}

		public void UpdateItemInCarts(CartItem item)
		{
			if (item == null)
			{
				throw new ArgumentNullException(nameof(item));
			}

			List<Models.Cart> cartsWithItem = this.dataService.GetAllWithItem(item.Id);
			if (cartsWithItem.Count == 0) return;

			foreach (Models.Cart cart in cartsWithItem)
			{
				CartItem cartItem = cart.Items.First(i => i.Id == item.Id);
				cartItem.Name = item.Name;
				cartItem.ImageUrl = item.ImageUrl;
				cartItem.ImageAlt = item.ImageAlt;
				cartItem.Price = item.Price;

				this.dataService.Update(cart);
			}
		}
	}
}

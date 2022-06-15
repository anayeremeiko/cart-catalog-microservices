namespace eShopServices.Services.Cart.Cart.API.Models
{
	/// <summary>
	/// The model of the cart.
	/// </summary>
	public class Cart
	{
		public Cart(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException(nameof(id));
			}

			this.Id = id;
			this.Items = new List<CartItem>();
		}

		/// <summary>
		/// The identifier of the cart. Generated on the client side.
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// The list of items in the cart.
		/// </summary>
		public ICollection<CartItem> Items { get; set; }

		/// <summary>
		/// Adds item to the cart. If item already exists, increases quantity of the item in the cart.
		/// </summary>
		/// <param name="item">Item to add to the cart.</param>
		public void AddItem(CartItem item)
		{
			if (item == null) return;

			CartItem existingItem = this.Items.FirstOrDefault(x => x.Id == item.Id);

			if (existingItem == null)
			{
				this.Items.Add(item);
			}
			else
			{
				existingItem.AddQuantity(item.Quantity);
			}
		}

		/// <summary>
		/// Removes item from the cart. If quantity of the item is higher than 1, decreases the quantity of the item.
		/// </summary>
		/// <param name="itemId">Item to remove from the cart.</param>
		public void RemoveItem(int itemId)
		{
			CartItem existingItem = this.Items.FirstOrDefault(x => x.Id == itemId);

			if (existingItem != null)
			{
				if (existingItem.Quantity == 1)
				{
					this.Items.Remove(existingItem);
				}
				else
				{
					existingItem.RemoveQuantity(1);
				}
			}
		}
	}
}

using Catalog.SharedKernel;
using Catalog.SharedKernel.Interfaces;

namespace Catalog.Core.Entities
{
	/// <summary>
	/// The item.
	/// </summary>
	public class Item : BaseEntity, IAggregateRoot
	{
		/*public Item(int id, string name, Category category, decimal price, int amount) : this(id, name, description: null, category, imageUrl: null, price, amount)
		{
		}

		public Item(int id, string name, string description, Category category, decimal price, int amount) : this(id, name, description, category, imageUrl: null, price, amount)
		{
		}

		public Item(int id, string name, Category category, string imageUrl, decimal price, int amount) : this(id, name, description: null, category, imageUrl, price, amount)
		{
		}

		public Item(int id, string name, string description, Category category, string imageUrl, decimal price, int amount)
		{
			Id = id;
			Name = name;
			Description = description;
			ImageUrl = imageUrl;
			Category = category;
			Price = price;
			Amount = amount;
		}*/

		/// <summary>
		/// The name of the item. Required.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The description of the item.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The url of the item image.
		/// </summary>
		public string ImageUrl { get; set; }

		/// <summary>
		/// The category that item belongs to. Required.
		/// </summary>
		public Category Category { get; set; }

		/// <summary>
		/// The price of the item. Required.
		/// </summary>
		public decimal Price { get; set; }

		/// <summary>
		/// The amount of the item. Required.
		/// </summary>
		public int Amount { get; set; }

		public bool EqualToItem(Item secondItem)
		{
			if (secondItem == null) return false;

			if (secondItem.Id == Id && 
				string.Compare(secondItem.Name, Name) == 0 && 
				string.Compare(secondItem.Description, Description) == 0 && 
				string.Compare(secondItem.ImageUrl, ImageUrl) == 0 && 
				secondItem.Price == Price && 
				secondItem.Amount == Amount) 
			{ 
				return true; 
			}

			return false;
		}
	}
}

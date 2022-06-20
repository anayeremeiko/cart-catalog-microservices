using Catalog.SharedKernel;
using Catalog.SharedKernel.Interfaces;

namespace Catalog.Core.Entities
{
	/// <summary>
	/// The category.
	/// </summary>
	public class Category : BaseEntity, IAggregateRoot
	{
		public Category(int id, string name) : this(id, name, imageUrl: null, parentCategory: null, items: null)
		{
		}

		public Category(int id, string name, string imageUrl) : this(id, name, imageUrl, parentCategory: null, items: null)
		{
		}

		public Category(int id, string name, Category parentCategory) : this(id, name, imageUrl: null, parentCategory, items: null)
		{
		}

		public Category(int id, string name, IEnumerable<Item> items) : this(id, name, imageUrl: null, parentCategory: null, items)
		{
		}

		public Category(int id, string name, string imageUrl, Category parentCategory) : this(id, name, imageUrl, parentCategory, items: null)
		{
		}

		public Category(int id, string name, string imageUrl, IEnumerable<Item> items) : this(id, name, imageUrl, parentCategory: null, items)
		{
		}

		public Category(int id, string name, Category parentCategory, IEnumerable<Item> items) : this(id, name, imageUrl: null, parentCategory, items)
		{
		}

		public Category(int id, string name, string imageUrl, Category parentCategory, IEnumerable<Item> items)
		{
			Id = id;
			Name = name;
			ImageUrl = imageUrl;
			ParentCategory = parentCategory;
			InternalItems = new List<Item>(items);
		}

		/// <summary>
		/// The name of the category. Required.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// The URL of the image.
		/// </summary>
		public string ImageUrl { get; private set; }

		/// <summary>
		/// The parent category of the category.
		/// </summary>
		public Category ParentCategory { get; private set; }

		private List<Item> InternalItems { get; set; }

		/// <summary>
		/// Items that category contains.
		/// </summary>
		public IReadOnlyCollection<Item> Items => this.InternalItems.AsReadOnly();
	}
}

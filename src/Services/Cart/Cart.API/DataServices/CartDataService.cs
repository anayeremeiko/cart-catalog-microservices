using eShopServices.Services.Cart.Cart.API.DataServices.Interfaces;
using LiteDB;

namespace eShopServices.Services.Cart.Cart.API.DataServices
{
	public class CartDataService : IDataService<Models.Cart>
	{
		private readonly ILiteRepository repository;

		public CartDataService(ILiteRepository repository)
		{
			this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
		}

		/// <summary>
		/// Adds cart to the database.
		/// </summary>
		/// <param name="entity">The cart that needs to be added.</param>
		public void Add(Models.Cart entity)
		{
			if (entity == null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			this.repository.Insert(entity);
		}

		/// <summary>
		/// Updates cart in the database.
		/// </summary>
		/// <param name="entity">The updated cart.</param>
		public void Update(Models.Cart entity)
		{
			if (entity == null) 
			{ 
				throw new ArgumentNullException(nameof(entity)); 
			}

			this.repository.Update(entity);
		}

		/// <summary>
		/// Finds cart with the provided id.
		/// </summary>
		/// <param name="id">The identifier of the entity.</param>
		/// <returns>The entity with provided id.</returns>
		public Models.Cart GetById(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException(nameof(id));
			}

			return this.repository.Query<Models.Cart>().Where(item => item.Id == id).FirstOrDefault();
		}

		public List<Models.Cart> GetAllWithItem(int itemId)
		{
			if (itemId < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(itemId));
			}

			return this.repository.Query<Models.Cart>().Where(cart => cart.Items.Select(i => i.Id).Any(id => id == itemId)).ToList();
		}
	}
}

namespace eShopServices.Services.Cart.Cart.API.DataServices.Interfaces
{
	/// <summary>
	/// Interface for service working with the infrastructure layer.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IDataService<T> where T : class
	{
		/// <summary>
		/// Finds entity with the provided id.
		/// </summary>
		/// <param name="id">The identifier of the entity.</param>
		/// <returns>The entity with provided id.</returns>
		T GetById(string id);

		/// <summary>
		/// Adds entity.
		/// </summary>
		/// <param name="entity">The entity that needs to be added.</param>
		void Add(T entity);

		/// <summary>
		/// Updates existing entity.
		/// </summary>
		/// <param name="entity">The updated entity.</param>
		void Update(T entity);
	}
}

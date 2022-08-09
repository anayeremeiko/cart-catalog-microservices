namespace Identity.Core.Services.Interfaces
{
	public interface IRepository<T>
	{
		Task<T> AddAsync(T entity);

		Task<T> UpdateAsync(T entity);

		Task<T> GetAsync<V>(V entityId);

		Task<IEnumerable<T>> GetAllAsync();
	}
}

namespace Catalog.SharedKernel.Interfaces
{
	public interface IRepository<T> where T: class, IAggregateRoot
	{
		Task<T> AddAsync(T entity);

		Task<T> UpdateAsync(T entity);

		Task DeleteAsync(T entity);
	}
}

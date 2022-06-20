namespace Catalog.SharedKernel.Interfaces
{
	public interface IReadRepository<T> where T : class, IAggregateRoot
	{
		Task<T> FindByIdAsync(int entityId);

		Task<IEnumerable<T>> GetAllAsync();
	}
}

namespace Catalog.SharedKernel.Interfaces
{
	public interface IReadRepository<T> where T : class, IAggregateRoot
	{
		Task<T> FindByIdAsync(int entityId);

		Task<IEnumerable<T>> GetAllAsync(Func<T, bool>? filteringCondition = null, int? pageSize = null, int? pageNumber = null);

		Task<int> CountAsync();
	}
}

using Catalog.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Data
{
	public class ReadRepository<T> : IReadRepository<T> where T : class, IAggregateRoot
	{
		private readonly DbContext appDbContext;

		public ReadRepository(DbContext dbContext)
		{
			appDbContext = dbContext;
		}

		public async Task<int> CountAsync()
		{
			var count = await appDbContext.Set<T>().CountAsync();

			return count;
		}

		public async Task<T> FindByIdAsync(int entityId)
		{
			var entity = await appDbContext.Set<T>().FindAsync(entityId);

			return entity;
		}

		public async Task<IEnumerable<T>> GetAllAsync(Func<T, bool>? filteringCondition = null, int? pageSize = null, int? pageNumber = null)
		{
			IEnumerable<T> entities;
			if (filteringCondition == null) {
				entities = await appDbContext.Set<T>().ToListAsync();
			} else {
				entities = appDbContext.Set<T>().Where(filteringCondition);
			}

			if (pageSize.HasValue && pageNumber.HasValue)
			{
				entities = await appDbContext.Set<T>().Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value).ToListAsync();
			}

			return entities;
		}
	}
}

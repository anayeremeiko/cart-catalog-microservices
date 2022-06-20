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

		public async Task<T> FindByIdAsync(int entityId)
		{
			var entity = await appDbContext.Set<T>().FindAsync(entityId);

			return entity;
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			var entities = await appDbContext.Set<T>().ToListAsync();

			return entities;
		}
	}
}

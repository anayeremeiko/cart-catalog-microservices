using Catalog.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Data
{
	public class Repository<T> : IRepository<T> where T : class, IAggregateRoot
	{
		private readonly DbContext appDbContext;

		public Repository(DbContext dbContext)
		{
			appDbContext = dbContext;
		}

		public async Task<T> AddAsync(T entity)
		{
			await appDbContext.Set<T>().AddAsync(entity);
			await appDbContext.SaveChangesAsync();

			return entity;
		}

		public async Task DeleteAsync(T entity)
		{
			appDbContext.Set<T>().Remove(entity);
			await appDbContext.SaveChangesAsync();
		}

		public async Task<T> UpdateAsync(T entity)
		{
			appDbContext.Entry(entity).State = EntityState.Modified;
			await appDbContext.SaveChangesAsync();

			return entity;
		}
	}
}

using Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Data
{
	public class ItemsRepository : Repository<Item>
	{
		private readonly DbContext appDbContext;

		public ItemsRepository(DbContext dbContext) : base(dbContext)
		{
			appDbContext = dbContext;
		}

		public new async Task<Item> UpdateAsync(Item entity)
		{
			appDbContext.Entry(entity).State = EntityState.Modified;
			appDbContext.Entry(entity).Property(i => i.Category).IsModified = false;
			await appDbContext.SaveChangesAsync();

			return entity;
		}
	}
}

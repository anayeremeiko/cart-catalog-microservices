using AutoMapper;
using Catalog.Core.Entities;
using Catalog.Infrastructure.Entities;
using Catalog.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Data
{
	public class ItemsRepository : IRepository<Item>
	{
		private readonly DbContext appDbContext;
		private readonly IMapper mapper;

		public ItemsRepository(DbContext dbContext, IMapper mapper)
		{
			appDbContext = dbContext;
			this.mapper = mapper;
		}

		public new async Task<Item> AddAsync(Item entity)
		{
			ItemDTO newItem = mapper.Map<ItemDTO>(entity);
			appDbContext.Entry(newItem).State = EntityState.Added;
			await appDbContext.SaveChangesAsync();

			return entity;
		}

		public new async Task DeleteAsync(Item entity)
		{
			ItemDTO itemToDelete = mapper.Map<ItemDTO>(entity);
			appDbContext.Entry(itemToDelete).State = EntityState.Deleted;
			await appDbContext.SaveChangesAsync();
		}

		public new async Task<Item> UpdateAsync(Item entity)
		{
			ItemDTO updatedItem = mapper.Map<ItemDTO>(entity);
			appDbContext.Entry(updatedItem).State = EntityState.Modified;
			await appDbContext.SaveChangesAsync();

			return entity;
		}
	}
}

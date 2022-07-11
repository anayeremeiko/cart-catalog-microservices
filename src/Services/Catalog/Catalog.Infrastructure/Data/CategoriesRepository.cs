using AutoMapper;
using Catalog.Core.Entities;
using Catalog.Infrastructure.Entities;
using Catalog.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Data
{
	public class CategoriesRepository : IRepository<Category>
	{
		private readonly DbContext appDbContext;
		private readonly IMapper mapper;

		public CategoriesRepository(DbContext dbContext, IMapper mapper)
		{
			appDbContext = dbContext;
			this.mapper = mapper;
		}

		public async Task<Category> AddAsync(Category entity)
		{
			CategoryDTO newCategory = mapper.Map<CategoryDTO>(entity);
			appDbContext.Entry(newCategory).State = EntityState.Added;
			await appDbContext.SaveChangesAsync();

			return entity;
		}

		public async Task DeleteAsync(Category entity)
		{
			CategoryDTO categoryToDelete = mapper.Map<CategoryDTO>(entity);
			appDbContext.Entry(categoryToDelete).State = EntityState.Deleted;
			await appDbContext.SaveChangesAsync();
		}

		public async Task<Category> UpdateAsync(Category entity)
		{
			CategoryDTO updatedCategory = mapper.Map<CategoryDTO>(entity);
			appDbContext.Entry(updatedCategory).State = EntityState.Modified;
			await appDbContext.SaveChangesAsync();

			return entity;
		}
	}
}

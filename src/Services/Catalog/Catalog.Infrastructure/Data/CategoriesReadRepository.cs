using AutoMapper;
using Catalog.Core.Entities;
using Catalog.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Data
{
	public class CategoriesReadRepository : SharedKernel.Interfaces.IReadRepository<Category>
	{
		private readonly DbContext appDbContext;
		private readonly IMapper mapper;

		public CategoriesReadRepository(DbContext dbContext, IMapper mapper)
		{
			this.appDbContext = dbContext;
			this.mapper = mapper;
		}

		public async Task<int> CountAsync()
		{
			var count = await appDbContext.Set<CategoryDTO>().CountAsync();

			return count;
		}

		public async Task<Category> FindByIdAsync(int entityId)
		{
			CategoryDTO category = await appDbContext.Set<CategoryDTO>().FindAsync(entityId);

			return mapper.Map<Category>(category);
		}

		public async Task<IEnumerable<Category>> GetAllAsync(Func<Category, bool>? filteringCondition = null, int? pageSize = null, int? pageNumber = null)
		{
			var categories = await appDbContext.Set<CategoryDTO>().Include(x => x.ParentCategory).ToListAsync();

			return categories.Select(x => {
				var category = mapper.Map<Category>(x);

				return category;
			});
		}
	}
}

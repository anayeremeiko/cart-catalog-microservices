using AutoMapper;
using Catalog.Core.Entities;
using Catalog.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Catalog.Infrastructure.Data
{
	public class ItemsReadRepository : SharedKernel.Interfaces.IReadRepository<Item>
	{
		private readonly DbContext appDbContext;
		private readonly IMapper mapper;

		public ItemsReadRepository(DbContext dbContext, IMapper mapper)
		{
			this.appDbContext = dbContext;
			this.mapper = mapper;
		}

		public async Task<int> CountAsync()
		{
			var count = await appDbContext.Set<ItemDTO>().CountAsync();

			return count;
		}

		public async Task<Item> FindByIdAsync(int entityId)
		{
			ItemDTO item = await appDbContext.Set<ItemDTO>().FindAsync(entityId);

			return mapper.Map<Item>(item);
		}

		public async Task<IEnumerable<Item>> GetAllAsync(Func<Item, bool>? filteringCondition = null, int? pageSize = null, int? pageNumber = null)
		{
			IEnumerable<ItemDTO> entities;
			if (filteringCondition == null)
			{
				entities = await appDbContext.Set<ItemDTO>().ToListAsync();
			}
			else
			{
				Func<ItemDTO, bool> filter = (x) => filteringCondition.Invoke(mapper.Map<Item>(x));
				entities = appDbContext.Set<ItemDTO>().Where(filter);
			}

			if (pageSize.HasValue && pageNumber.HasValue)
			{
				entities = entities.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
			}

			return entities.Select(x => mapper.Map<Item>(x));
		}
	}
}

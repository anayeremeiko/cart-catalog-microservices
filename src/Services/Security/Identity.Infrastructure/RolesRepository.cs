using AutoMapper;
using Identity.Core.Entities;
using Identity.Core.Services.Interfaces;
using Identity.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure
{
	public class RolesRepository : IRepository<Role>
	{
		private readonly DbContext appDbContext;
		private readonly IMapper mapper;

		public RolesRepository(DbContext dbContext, IMapper mapper)
		{
			appDbContext = dbContext;
			this.mapper = mapper;
		}

		public async Task<Role> AddAsync(Role entity)
		{
			UserRole newRole = mapper.Map<UserRole>(entity);
			appDbContext.Entry(newRole).State = EntityState.Added;
			await appDbContext.SaveChangesAsync();

			return entity;
		}

		public async Task<IEnumerable<Role>> GetAllAsync()
		{
			var userRoles = await appDbContext.Set<UserRole>().ToListAsync();
			var roles = userRoles.Select(x => mapper.Map<Role>(x));

			return roles;
		}

		public async Task<Role> GetAsync<V>(V entityId)
		{
			UserRole role = await appDbContext.Set<UserRole>().FindAsync(entityId);

			return mapper.Map<Role>(role);
		}

		public async Task<Role> UpdateAsync(Role entity)
		{
			UserRole updatedRole = mapper.Map<UserRole>(entity);
			appDbContext.Entry(updatedRole).State = EntityState.Modified;
			await appDbContext.SaveChangesAsync();

			return entity;
		}
	}
}

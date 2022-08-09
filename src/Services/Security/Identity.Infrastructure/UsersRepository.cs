using AutoMapper;
using Identity.Core.Entities;
using Identity.Core.Services.Interfaces;
using Identity.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure
{
	public class UsersRepository : IRepository<User>
	{
		private readonly DbContext appDbContext;
		private readonly IMapper mapper;

		public UsersRepository(DbContext dbContext, IMapper mapper)
		{
			appDbContext = dbContext;
			this.mapper = mapper;
		}

		public async Task<User> AddAsync(User entity)
		{
			UserInformation newUser = mapper.Map<UserInformation>(entity);
			appDbContext.Entry(newUser).State = EntityState.Added;
			await appDbContext.SaveChangesAsync();

			return entity;
		}

		public Task<IEnumerable<User>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<User> GetAsync<V>(V entityId)
		{
			UserInformation user = await appDbContext.Set<UserInformation>().FindAsync(entityId);

			return mapper.Map<User>(user);
		}

		public async Task<User> UpdateAsync(User entity)
		{
			UserInformation updatedUser = mapper.Map<UserInformation>(entity);
			appDbContext.Entry(updatedUser).State = EntityState.Modified;
			await appDbContext.SaveChangesAsync();

			return entity;
		}
	}
}

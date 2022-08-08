using Identity.Core.Entities;
using Identity.Core.Services.Interfaces;

namespace Identity.Core.Services
{
	public class UsersService : IUsersService
	{
		private readonly IRepository<User> userRepository;

		public UsersService(IRepository<User> userRepository)
		{
			this.userRepository = userRepository;
		}

		public async Task AddUserAsync(User user)
		{
			await this.userRepository.AddAsync(user);
		}

		public async Task<User> GetUserAsync(string userName)
		{
			User user = await this.userRepository.GetAsync<string>(userName);

			return user;
		}

		public async Task<User> UpdateUserAsync(User user)
		{
			User updatedUser = await this.userRepository.UpdateAsync(user);

			return updatedUser;
		}
	}
}

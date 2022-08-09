using Identity.Core.Entities;

namespace Identity.Core.Services.Interfaces
{
	public interface IUsersService
	{
		Task<User> GetUserAsync(string userName);

		Task AddUserAsync(User user);

		Task<User> UpdateUserAsync(User user);
	}
}

using Identity.Core.Entities;

namespace Identity.Core.Services.Interfaces
{
	public interface IRolesService
	{
		Task<IEnumerable<Role>> GetRolesAsync();

		Task<Role> GetRoleAsync(int roleId);

		Task<Role> AddRoleAsync(Role role);

		Task<Role> UpdateRoleAsync(Role role);
	}
}

using Identity.Core.Entities;
using Identity.Core.Services.Interfaces;

namespace Identity.Core.Services
{
	public class RolesService : IRolesService
	{
		private readonly IRepository<Role> rolesRepository;

		public RolesService(IRepository<Role> repostory)
		{
			this.rolesRepository = repostory;
		}

		public async Task<Role> AddRoleAsync(Role role)
		{
			Role createdRole = await this.rolesRepository.AddAsync(role);

			return createdRole;
		}

		public async Task<Role> GetRoleAsync(int roleId)
		{
			Role role = await this.rolesRepository.GetAsync(roleId);

			return role;
		}

		public async Task<IEnumerable<Role>> GetRolesAsync()
		{
			IEnumerable<Role> roles = await this.rolesRepository.GetAllAsync();

			return roles;
		}

		public async Task<Role> UpdateRoleAsync(Role role)
		{
			Role updatedRole = await this.rolesRepository.UpdateAsync(role);

			return updatedRole;
		}
	}
}

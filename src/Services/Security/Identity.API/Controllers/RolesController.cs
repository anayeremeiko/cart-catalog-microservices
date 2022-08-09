using Identity.API.Models;
using Identity.Core.Entities;
using Identity.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RolesController : ControllerBase
	{
        private readonly IRolesService roleManager;
        private readonly IUsersService usersService;

        public RolesController(IRolesService roleManager, IUsersService usersService)
        {
            this.roleManager = roleManager;
            this.usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await roleManager.GetRolesAsync();

            return Ok(roles);
        }

		[HttpGet("{id}")]
        public async Task<IActionResult> GetRole(int id)
		{
            var role = await roleManager.GetRoleAsync(id);

            return Ok(role);
		}

        [HttpPost]
        public async Task<IActionResult> AddRole(int id, RoleRequest request)
        {
            Role role = new Role
            {
                Id = id,
                Name = request.Name,
                CanCreate = request.CanCreate,
                CanRead = request.CanRead,
                CanDelete = request.CanDelete,
                CanEdit = request.CanEdit
            };
            await roleManager.AddRoleAsync(role);

            return Created($"api/[controller]/{id}", role);
        }

		[HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, RoleRequest request)
		{
            Role role = new Role
            {
                Id = id,
                Name = request.Name,
                CanCreate = request.CanCreate,
                CanRead = request.CanRead,
                CanDelete = request.CanDelete,
                CanEdit = request.CanEdit
            };

            var updatedRole = await roleManager.UpdateRoleAsync(role);

            return Ok(updatedRole);
        }
    }
}

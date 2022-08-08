using Identity.Core.Entities;
using Shared.Models;

namespace Identity.Core.Services.Interfaces
{
	public interface ITokenGenerator
	{
		string CreateToken(User user);

		Task<bool> ValidateAsync(string token, UserPermissions requiredPermission);
	}
}

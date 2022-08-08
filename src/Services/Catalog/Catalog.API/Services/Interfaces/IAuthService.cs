using Shared.Models;

namespace Catalog.API.Services.Interfaces
{
	/// <summary>
	/// Service for validating user auth token
	/// </summary>
	public interface IAuthService
	{
		/// <summary>
		/// Validate user auth token
		/// </summary>
		/// <param name="bearerToken">JWT token</param>
		/// <param name="requiredPermission">Required permission for accessing endpoint</param>
		/// <returns></returns>
		Task<bool> ValidateUserToken(string bearerToken, UserPermissions requiredPermission);
	}
}

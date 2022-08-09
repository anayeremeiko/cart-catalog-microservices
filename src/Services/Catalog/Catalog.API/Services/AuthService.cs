using Catalog.API.Services.Interfaces;
using Shared.Models;

namespace Catalog.API.Services
{
	public class AuthService : IAuthService
	{
		private const string BearerPrefix = "Bearer ";
		private readonly HttpClient httpClient;

		public AuthService()
		{
			this.httpClient = new HttpClient();
		}

		public async Task<bool> ValidateUserToken(string bearerToken, UserPermissions requiredPermission)
		{
			if (!string.IsNullOrEmpty(bearerToken) && bearerToken.StartsWith(BearerPrefix))
			{
				bearerToken = bearerToken.Substring(BearerPrefix.Length);
			}

			var response = await this.httpClient.GetAsync(new Uri($"https://localhost:7020/validateToken/{bearerToken}?requiredPermission={requiredPermission}"));
			bool isValid = await response.Content.ReadAsAsync<bool>();

			return isValid;
		}
	}
}

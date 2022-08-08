using Identity.API.Models;
using Identity.Core.Entities;
using Identity.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Identity.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly ITokenGenerator tokenGenerator;
		private readonly IHashingService hashingService;
		private readonly IUsersService usersService;

		public AuthController(ITokenGenerator tokenGenerator, IHashingService hashingService, IUsersService usersService)
		{
			this.tokenGenerator = tokenGenerator;
			this.hashingService = hashingService;
			this.usersService = usersService;
		}

		[HttpPost("~/register", Name = "Register")]
		public async Task<IActionResult> Register(UserCredentialsRequest request)
		{
			(byte[] hash, byte[] salt) hashSalt = this.hashingService.CreateHash(request.Password);
			User user = new User()
			{
				UserName = request.UserName,
				PasswordHash = hashSalt.hash,
				PasswordSalt = hashSalt.salt
			};
			await usersService.AddUserAsync(user);

			return Ok();
		}

		[HttpPost("~/login", Name = "Login")]
		public async Task<IActionResult> Login(UserCredentialsRequest request)
		{
			var user = await usersService.GetUserAsync(request.UserName);
			bool isValid = this.hashingService.ValidateHash(request.Password, user.PasswordHash, user.PasswordSalt);
			
			if (!isValid) return Unauthorized();

			string token = this.tokenGenerator.CreateToken(user);
			
			return Ok(token);
		}

		[HttpGet("~/validateToken/{token}")]
		public async Task<IActionResult> ValidateToken(string token, UserPermissions requiredPermission)
		{
			bool isValid = await this.tokenGenerator.ValidateAsync(token, requiredPermission);

			return Ok(isValid);
		}
	}
}
using Identity.Core.Entities;
using Identity.Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Core.Services
{
	public class TokenGenerator : ITokenGenerator
	{
		private readonly IConfiguration configuration;

		public TokenGenerator(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public string CreateToken(User user)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.Role, user.UserRole.Name.ToString()),
				new Claim("Read", user.UserRole.CanRead.ToString()),
				new Claim("Create", user.UserRole.CanCreate.ToString()),
				new Claim("Update", user.UserRole.CanEdit.ToString()),
				new Claim("Delete", user.UserRole.CanDelete.ToString())
			};

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JWT:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
			JwtSecurityToken token = new JwtSecurityToken(
				issuer: this.configuration["JWT:Issuer"], 
				claims: claims, 
				expires: DateTime.Now.AddMinutes(15), 
				signingCredentials: credentials);
			string jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return jwt;
		}

		public async Task<bool> ValidateAsync(string token, UserPermissions requiredPermission)
		{
			if (token == null)
			{
				return false;
			}

			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JWT:Key"]));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

			var tokenHadler = new JwtSecurityTokenHandler();
			TokenValidationResult validationResult = await tokenHadler.ValidateTokenAsync(token, new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = securityKey,
				ValidateIssuer = false,
				ValidateAudience = false,
				ClockSkew = TimeSpan.Zero
			});

			var isValid = validationResult.IsValid && bool.Parse((string)validationResult.Claims[requiredPermission.ToString()]);

			return isValid;
		}
	}
}

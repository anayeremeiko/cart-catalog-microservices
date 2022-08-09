using Catalog.API.Models;
using Catalog.API.Services.Interfaces;
using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Shared.Models;

namespace Catalog.API.Controllers
{
	[ApiController]
	[Route("api/categories")]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryService categoryService;
		private readonly IAuthService authService;

		public CategoriesController(ICategoryService service, IAuthService authService)
		{
			categoryService = service;
			this.authService = authService;
		}

		/// <summary>
		/// Get a list of all categories
		/// </summary>
		/// <returns>A list of categories</returns>
		[HttpGet()]
		[AllowAnonymous]
		public async Task<IEnumerable<Category>> GetCategories()
		{
			var categories = await categoryService.ListCategoriesAsync();

			return categories;
		}

		/// <summary>
		/// Add new category
		/// </summary>
		/// <param name="id">New category identifier.</param>
		/// <param name="category">New category parameters</param>
		/// <returns>Created category</returns>
		[HttpPost("{id}")]
		public async Task<IActionResult> AddCategory(int id, UpdatedCategory category)
		{
			IActionResult? result = await this.AuthorizeUser(UserPermissions.Create);
			if (result != null) return result;

			var addedCategory = await categoryService.AddCategoryAsync(id, category.Name, category.ImageUrl, category.ParentCategoryId);

			return Created($"api/categories/{id}", addedCategory);
		}

		/// <summary>
		/// Update existing category
		/// </summary>
		/// <param name="id">Category identifier.</param>
		/// <param name="category">Updated category parameters.</param>
		/// <returns>Updated category</returns>
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCategory(int id, UpdatedCategory category)
		{
			IActionResult? result = await this.AuthorizeUser(UserPermissions.Update);
			if (result != null) return result;

			var updatedCategory = await categoryService.UpdateCategoryAsync(id, category.Name, category.ImageUrl, category.ParentCategoryId);

			return Ok(updatedCategory);
		}

		/// <summary>
		/// Delete existing category
		/// </summary>
		/// <param name="id">Category identifier</param>
		/// <returns></returns>
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			IActionResult? result = await this.AuthorizeUser(UserPermissions.Delete);
			if (result != null) return result;

			var category = await categoryService.GetCategoryAsync(id);
			await categoryService.DeleteCategoryAsync(category);

			return NoContent();
		}

		private async Task<IActionResult?> AuthorizeUser(UserPermissions permission)
		{
			this.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
			if (string.IsNullOrEmpty(headerValue)) return Unauthorized();

			bool isAuthorized = await this.authService.ValidateUserToken(headerValue, permission);
			if (!isAuthorized) return Problem(
				type: "/docs/errors/forbidden",
				title: "Authenticated user is not authorized.",
				detail: "Token is not valid or user does not have rights to access the endpoint.",
				statusCode: StatusCodes.Status403Forbidden,
				instance: HttpContext.Request.Path
			);

			return null;
		}
	}
}
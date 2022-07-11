using Catalog.API.Models;
using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
	[ApiController]
	[Route("api/categories")]
	public class CategoriesController : ControllerBase
	{
		private readonly ICategoryService categoryService;

		public CategoriesController(ICategoryService service)
		{
			categoryService = service;
		}

		/// <summary>
		/// Get a list of all categories
		/// </summary>
		/// <returns>A list of categories</returns>
		[HttpGet()]
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
			var category = await categoryService.GetCategoryAsync(id);
			await categoryService.DeleteCategoryAsync(category);

			return NoContent();
		}
	}
}
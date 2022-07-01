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

		[HttpGet()]
		public async Task<IEnumerable<Category>> GetCategories()
		{
			var categories = await categoryService.ListCategoriesAsync();

			return categories;
		}

		[HttpPost("{id}")]
		public async Task<IActionResult> AddCategory(int id, Category category)
		{
			if (category.Id != id)
			{
				return BadRequest();
			}

			try
			{
				var addedCategory = await categoryService.AddCategoryAsync(category);

				return Created($"api/categories/{id}", addedCategory);
			} catch(Exception exception)
			{
				return StatusCode(500, exception.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCategory(int id, Category category)
		{
			if (category.Id != id)
			{
				return BadRequest();
			}

			try
			{
				var updatedCategory = await categoryService.UpdateCategoryAsync(category);

				return Ok(updatedCategory);
			}
			catch (Exception exception)
			{
				return StatusCode(500, exception.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			var category = await categoryService.GetCategoryAsync(id);
			await categoryService.DeleteCategoryAsync(category);

			return NoContent();
		}
	}
}
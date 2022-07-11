using Catalog.Core.Entities;

namespace Catalog.Core.Interfaces
{
	public interface ICategoryService
	{
		Task<Category> GetCategoryAsync(int caterogyId);

		Task<IEnumerable<Category>> ListCategoriesAsync();

		Task<Category> AddCategoryAsync(int id, string name, string imageUrl, int? parentCategoryId);

		Task<Category> UpdateCategoryAsync(int id, string name, string imageUrl, int? parentCategoryId);

		Task DeleteCategoryAsync(Category category);
	}
}

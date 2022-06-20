﻿using Catalog.Core.Entities;

namespace Catalog.Core.Interfaces
{
	public interface ICategoryService
	{
		Task<Category> GetCategoryAsync(int caterogyId);

		Task<IEnumerable<Item>> ListCaterogyAsync(int caterogyId);

		Task<Category> AddCategoryAsync(Category category);

		Task<Category> UpdateCategoryAsync(Category category);

		Task DeleteCategoryAsync(Category category);
	}
}

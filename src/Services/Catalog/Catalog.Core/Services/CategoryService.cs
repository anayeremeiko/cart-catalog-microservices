using Catalog.Core.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Catalog.Core.Queries;
using Catalog.Core.Validators;
using FluentValidation;
using MediatR;

namespace Catalog.Core.Services
{
	/// <summary>
	/// Service for working with categories.
	/// </summary>
	public class CategoryService : ICategoryService
	{
		private readonly IMediator mediator;

		public CategoryService(IMediator mediator)
		{
			this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		/// <summary>
		/// Adds category.
		/// </summary>
		/// <param name="category">New category.</param>
		/// <returns>Added category.</returns>
		public async Task<Category> AddCategoryAsync(Category category)
		{
			var validator = new CategoryValidator();
			await validator.ValidateAndThrowAsync(category);

			CreateCategoryCommand request = new CreateCategoryCommand() 
			{ 
				NewCategory = category 
			};
			var addedCategory = await mediator.Send(request);

			return addedCategory;
		}

		/// <summary>
		/// Deletes the category.
		/// </summary>
		/// <param name="category">Category to delete.</param>
		public async Task DeleteCategoryAsync(Category category)
		{
			DeleteCategoryCommand request = new DeleteCategoryCommand()
			{
				Category = category
			};
			
			await mediator.Send(request);
		}

		/// <summary>
		/// Gets category by  category identifier.
		/// </summary>
		/// <param name="caterogyId">The category identifier.</param>
		/// <returns>Category with the provided identifier.</returns>
		public async Task<Category> GetCategoryAsync(int caterogyId)
		{
			GetCategoryQuery request = new GetCategoryQuery() 
			{ 
				Id = caterogyId 
			};
			var category = await mediator.Send(request);

			return category;
		}

		/// <summary>
		/// Lists category items.
		/// </summary>
		/// <param name="caterogyId">The category identifier.</param>
		/// <returns>Items that belong to the category with the provided identifier.</returns>
		public async Task<IEnumerable<Item>> ListCaterogyAsync(int caterogyId)
		{
			GetCategoryQuery request = new GetCategoryQuery()
			{
				Id = caterogyId
			};
			var category = await mediator.Send(request);

			return category.Items;
		}

		/// <summary>
		/// Updates category.
		/// </summary>
		/// <param name="category">Updated category.</param>
		/// <returns>Updated category.</returns>
		public async Task<Category> UpdateCategoryAsync(Category category)
		{
			var validator = new CategoryValidator();
			await validator.ValidateAndThrowAsync(category);

			Category currentCategory = await this.GetCategoryAsync(category.Id);

			UpdateCategoryCommand request = new UpdateCategoryCommand()
			{
				UpdatedCategory = category,
				CurrentCategory = currentCategory
			};
			var updatedCategory = await mediator.Send(request);

			return updatedCategory;
		}
	}
}

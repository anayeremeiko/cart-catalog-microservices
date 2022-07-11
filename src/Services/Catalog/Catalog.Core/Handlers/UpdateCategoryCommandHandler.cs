using Catalog.Core.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Catalog.SharedKernel.Interfaces;
using MediatR;

namespace Catalog.Core.Handlers
{
	public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Category>
	{
		private readonly IRepository<Category> categoryRepository;
		private readonly IItemService itemService;

		public UpdateCategoryCommandHandler(IRepository<Category> categoryRepository, IItemService service)
		{
			this.categoryRepository = categoryRepository;
			this.itemService = service;
		}

		public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
		{
			await this.categoryRepository.UpdateAsync(request.UpdatedCategory);

			var updatedItems = request.UpdatedCategory.Items?.ToList();

			if (updatedItems == null)
			{
				return request.UpdatedCategory;
			}

			var existingItemsIds = updatedItems?.Select(x => x.Id).Intersect(request.CurrentCategory.Items.Select(i => i.Id));

			foreach (var item in updatedItems)
			{
				if (!existingItemsIds.Contains(item.Id))
				{
					await itemService.AddItemAsync(item.Id, item.Name, item.Description, item.ImageUrl, item.Category.Id, item.Price, item.Amount);
				}
			}

			foreach (var item in request.CurrentCategory.Items)
			{
				var updatedItem = updatedItems.Find(i => i.Id == item.Id);
				if (updatedItem == null)
				{
					await itemService.DeleteItemAsync(item);
				}
				else
				{
					if (!item.EqualToItem(updatedItem))
					{
						await itemService.UpdateItemAsync(updatedItem.Id, updatedItem.Name, updatedItem.Description, updatedItem.ImageUrl, updatedItem.Category.Id, updatedItem.Price, updatedItem.Amount);
					}
				}
			}

			return request.UpdatedCategory;
		}
	}
}

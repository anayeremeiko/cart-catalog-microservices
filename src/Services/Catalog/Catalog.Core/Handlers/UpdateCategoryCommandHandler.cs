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

			var updatedItems = request.UpdatedCategory.Items.ToList();

			var existingItemsIds = updatedItems.Select(x => x.Id).Intersect(request.CurrentCategory.Items.Select(i => i.Id));
			
			foreach(var item in updatedItems)
			{
				if (!existingItemsIds.Contains(item.Id))
				{
					await itemService.AddItemAsync(item);
				}
			}

			foreach (var item in request.CurrentCategory.Items) {
				var updatedItem = updatedItems.Find(i => i.Id == item.Id);
				if (updatedItem == null)
				{
					await itemService.DeleteItemAsync(item);
				} else
				{
					if (!item.EqualToItem(updatedItem))
					{
						await itemService.UpdateItemAsync(updatedItem);
					}
				}
			}
			
			return request.UpdatedCategory;
		}
	}
}

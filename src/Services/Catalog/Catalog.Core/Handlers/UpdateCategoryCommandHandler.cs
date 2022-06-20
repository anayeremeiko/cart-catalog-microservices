using Catalog.Core.Commands;
using Catalog.Core.Entities;
using Catalog.SharedKernel.Interfaces;
using MediatR;

namespace Catalog.Core.Handlers
{
	public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Category>
	{
		private readonly IRepository<Category> categoryRepository;
		private readonly IRepository<Item> itemRepository;

		public UpdateCategoryCommandHandler(IRepository<Category> categoryRepository, IRepository<Item> itemRepository)
		{
			this.categoryRepository = categoryRepository;
			this.itemRepository = itemRepository;
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
					await itemRepository.AddAsync(item);
				}
			}

			foreach (var item in request.CurrentCategory.Items) {
				var updatedItem = updatedItems.Find(i => i.Id == item.Id);
				if (updatedItem == null)
				{
					await itemRepository.DeleteAsync(item);
				} else
				{
					if (!item.EqualToItem(updatedItem))
					{
						await itemRepository.UpdateAsync(updatedItem);
					}
				}
			}
			
			return request.UpdatedCategory;
		}
	}
}

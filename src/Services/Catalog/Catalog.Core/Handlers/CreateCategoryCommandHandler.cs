using Catalog.Core.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Catalog.SharedKernel.Interfaces;
using MediatR;

namespace Catalog.Core.Handlers
{
	public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
	{
		private readonly IRepository<Category> categoryRepository;
		private readonly IItemService itemService;

		public CreateCategoryCommandHandler(IRepository<Category> categoryRepository, IItemService service)
		{
			this.categoryRepository = categoryRepository;
			this.itemService = service;
		}

		public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
		{
			await categoryRepository.AddAsync(request.NewCategory);
			if (request.NewCategory.Items?.Count > 0)
			{
				foreach(var item in request.NewCategory.Items)
				{
					await itemService.AddItemAsync(item.Id, item.Name, item.Description, item.ImageUrl, item.Category.Id, item.Price, item.Amount);
				}
			}

			return request.NewCategory;
		}
	}
}

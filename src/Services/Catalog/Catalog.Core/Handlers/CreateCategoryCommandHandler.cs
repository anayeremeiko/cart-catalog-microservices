using Catalog.Core.Commands;
using Catalog.Core.Entities;
using Catalog.SharedKernel.Interfaces;
using MediatR;

namespace Catalog.Core.Handlers
{
	public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
	{
		private readonly IRepository<Category> categoryRepository;
		private readonly IRepository<Item> itemRepository;

		public CreateCategoryCommandHandler(IRepository<Category> categoryRepository, IRepository<Item> itemRepository)
		{
			this.categoryRepository = categoryRepository;
			this.itemRepository = itemRepository;
		}

		public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
		{
			await categoryRepository.AddAsync(request.NewCategory);
			if (request.NewCategory.Items.Count > 0)
			{
				foreach(var item in request.NewCategory.Items)
				{
					await itemRepository.AddAsync(item);
				}
			}

			return request.NewCategory;
		}
	}
}

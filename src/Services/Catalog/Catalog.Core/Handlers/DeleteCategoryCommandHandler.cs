using Catalog.Core.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Interfaces;
using Catalog.SharedKernel.Interfaces;
using MediatR;

namespace Catalog.Core.Handlers
{
	public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
	{
		private readonly IRepository<Category> categoryRepository;
		private readonly IItemService itemService;

		public DeleteCategoryCommandHandler(IRepository<Category> categoryRepository, IItemService service)
		{
			this.categoryRepository = categoryRepository;
			this.itemService = service;
		}

		public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
		{
			await categoryRepository.DeleteAsync(request.Category);
			if (request.Category.Items.Count > 0)
			{
				foreach (var item in request.Category.Items)
				{
					await itemService.DeleteItemAsync(item);
				}
			}

			return Unit.Value;
		}
	}
}

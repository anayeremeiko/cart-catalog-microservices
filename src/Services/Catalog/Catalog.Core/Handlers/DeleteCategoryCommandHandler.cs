using Catalog.Core.Commands;
using Catalog.Core.Entities;
using Catalog.SharedKernel.Interfaces;
using MediatR;

namespace Catalog.Core.Handlers
{
	public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
	{
		private readonly IRepository<Category> categoryRepository;
		private readonly IRepository<Item> itemRepository;

		public DeleteCategoryCommandHandler(IRepository<Category> categoryRepository, IRepository<Item> itemRepository)
		{
			this.categoryRepository = categoryRepository;
			this.itemRepository = itemRepository;
		}

		public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
		{
			await categoryRepository.DeleteAsync(request.Category);
			if (request.Category.Items.Count > 0)
			{
				foreach (var item in request.Category.Items)
				{
					await itemRepository.DeleteAsync(item);
				}
			}

			return Unit.Value;
		}
	}
}

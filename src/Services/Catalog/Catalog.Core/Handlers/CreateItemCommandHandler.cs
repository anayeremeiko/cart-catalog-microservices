using Catalog.Core.Commands;
using Catalog.Core.Entities;
using Catalog.SharedKernel.Interfaces;
using MediatR;

namespace Catalog.Core.Handlers
{
	public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Item>
	{
		private readonly IRepository<Item> itemRepository;

		public CreateItemCommandHandler(IRepository<Item> itemRepository)
		{
			this.itemRepository = itemRepository;
		}

		public async Task<Item> Handle(CreateItemCommand request, CancellationToken cancellationToken)
		{
			await itemRepository.AddAsync(request.NewItem);

			return request.NewItem;
		}
	}
}

using Catalog.Core.Commands;
using Catalog.Core.Entities;
using Catalog.SharedKernel.Interfaces;
using MediatR;

namespace Catalog.Core.Handlers
{
	public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, Unit>
	{
		private readonly IRepository<Item> itemRepository;

		public DeleteItemCommandHandler(IRepository<Item> itemRepository)
		{
			this.itemRepository = itemRepository;
		}

		public async Task<Unit> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
		{
			await itemRepository.DeleteAsync(request.Item);

			return Unit.Value;
		}
	}
}

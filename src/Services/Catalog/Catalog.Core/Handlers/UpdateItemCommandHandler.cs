using Catalog.Core.Commands;
using Catalog.Core.Entities;
using Catalog.SharedKernel.Interfaces;
using MediatR;

namespace Catalog.Core.Handlers
{
	public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Item>
	{
		private readonly IRepository<Item> itemRepository;

		public UpdateItemCommandHandler(IRepository<Item> itemRepository)
		{
			this.itemRepository = itemRepository;
		}

		public async Task<Item> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
		{
			await this.itemRepository.UpdateAsync(request.UpdatedItem);

			return request.UpdatedItem;
		}
	}
}

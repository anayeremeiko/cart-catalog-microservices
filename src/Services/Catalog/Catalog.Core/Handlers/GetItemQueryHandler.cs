using Catalog.Core.Entities;
using Catalog.Core.Queries;
using Catalog.SharedKernel.Interfaces;
using MediatR;

namespace Catalog.Core.Handlers
{
	public class GetItemQueryHandler : IRequestHandler<GetItemQuery, Item>
	{
		private readonly IReadRepository<Item> repository;

		public GetItemQueryHandler(IReadRepository<Item> repository)
		{
			this.repository = repository;
		}

		public async Task<Item> Handle(GetItemQuery request, CancellationToken cancellationToken)
		{
			return await repository.FindByIdAsync(request.Id);
		}
	}
}

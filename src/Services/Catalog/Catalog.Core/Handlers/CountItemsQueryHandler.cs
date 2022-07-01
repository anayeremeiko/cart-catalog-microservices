using Catalog.Core.Entities;
using Catalog.Core.Queries;
using Catalog.SharedKernel.Interfaces;
using MediatR;

namespace Catalog.Core.Handlers
{
	internal class CountItemsQueryHandler : IRequestHandler<CountItemsQuery, int>
	{
		private readonly IReadRepository<Item> repository;

		public CountItemsQueryHandler(IReadRepository<Item> repository)
		{
			this.repository = repository;
		}

		public async Task<int> Handle(CountItemsQuery request, CancellationToken cancellationToken)
		{
			return await repository.CountAsync();
		}
	}
}

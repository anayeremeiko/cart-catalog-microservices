using Catalog.Core.Entities;
using Catalog.Core.Queries;
using Catalog.SharedKernel.Interfaces;
using MediatR;

namespace Catalog.Core.Handlers
{
	public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, IEnumerable<Item>>
	{
		private readonly IReadRepository<Item> repository;

		public GetItemsQueryHandler(IReadRepository<Item> repository)
		{
			this.repository = repository;
		}

		public async Task<IEnumerable<Item>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
		{
			return await repository.GetAllAsync(filteringCondition: (item) =>
			{
				return item.Category.Id == request.CategoryIdToFilterBy.Value;
			}, request.PageSize, request.PageNumber);
		}
	}
}

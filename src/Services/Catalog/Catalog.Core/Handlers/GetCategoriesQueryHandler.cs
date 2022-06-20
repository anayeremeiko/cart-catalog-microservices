using Catalog.Core.Entities;
using Catalog.Core.Queries;
using Catalog.SharedKernel.Interfaces;
using MediatR;

namespace Catalog.Core.Handlers
{
	public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<Category>>
	{
		private readonly IReadRepository<Category> repository;

		public GetCategoriesQueryHandler(IReadRepository<Category> repository)
		{
			this.repository = repository;
		}

		public async Task<IEnumerable<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
		{
			return await repository.GetAllAsync();
		}
	}
}

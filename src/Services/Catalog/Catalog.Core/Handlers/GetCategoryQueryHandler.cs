using Catalog.Core.Entities;
using Catalog.Core.Queries;
using Catalog.SharedKernel.Interfaces;
using MediatR;

namespace Catalog.Core.Handlers
{
	public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Category>
	{
		private readonly IReadRepository<Category> repository;

		public GetCategoryQueryHandler(IReadRepository<Category> repository)
		{
			this.repository = repository;
		}

		public async Task<Category> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
		{
			return await repository.FindByIdAsync(request.Id);
		}
	}
}

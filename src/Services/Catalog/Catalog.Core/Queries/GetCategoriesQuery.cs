using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Core.Queries
{
	public class GetCategoriesQuery : IRequest<IEnumerable<Category>>
	{
	}
}

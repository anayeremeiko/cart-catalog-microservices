using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Core.Queries
{
	public class GetItemsQuery : IRequest<IEnumerable<Item>>
	{
	}
}

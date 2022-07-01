using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Core.Queries
{
	public class GetItemsQuery : IRequest<IEnumerable<Item>>
	{
		public int? PageSize { get; set; }

		public int? PageNumber { get; set; }

		public int? CategoryIdToFilterBy { get; set; }
	}
}

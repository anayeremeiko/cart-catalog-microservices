using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Core.Queries
{
	public class GetItemQuery : IRequest<Item>
	{
		public int Id { get; set; }
	}
}

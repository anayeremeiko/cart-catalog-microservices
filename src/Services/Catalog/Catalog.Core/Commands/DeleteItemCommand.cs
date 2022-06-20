using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Core.Commands
{
	public class DeleteItemCommand : IRequest
	{
		public Item Item { get; set; }
	}
}

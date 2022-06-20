using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Core.Commands
{
	public class CreateItemCommand : IRequest<Item>
	{
		public Item NewItem { get; set; }
	}
}

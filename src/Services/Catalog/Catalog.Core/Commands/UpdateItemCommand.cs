using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Core.Commands
{
	public class UpdateItemCommand : IRequest<Item>
	{
		public Item UpdatedItem { get; set; }
	}
}

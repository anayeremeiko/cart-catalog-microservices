using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Core.Commands
{
	public class DeleteCategoryCommand : IRequest
	{
		public Category Category { get; set; }
	}
}

using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Core.Commands
{
	public class CreateCategoryCommand : IRequest<Category>
	{
		public Category NewCategory { get; set; }
	}
}

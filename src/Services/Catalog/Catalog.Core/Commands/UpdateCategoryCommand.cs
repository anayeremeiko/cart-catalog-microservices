using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Core.Commands
{
	public class UpdateCategoryCommand : IRequest<Category>
	{
		public Category UpdatedCategory { get; set; }

		public Category CurrentCategory { get; set; }
	}
}

using Catalog.Core.Entities;
using MediatR;

namespace Catalog.Core.Queries
{
	public  class GetCategoryQuery : IRequest<Category>
	{
		public int Id { get; set; }
	}
}

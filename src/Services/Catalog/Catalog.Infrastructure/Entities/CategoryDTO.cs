using Catalog.Core.Entities;
using Catalog.SharedKernel.Interfaces;

namespace Catalog.Infrastructure.Entities
{
	public class CategoryDTO : IAggregateRoot
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string? ImageUrl { get; set; }

		public int? ParentCategoryId { get; set; }

		public virtual CategoryDTO? ParentCategory { get; set; }

		public virtual List<ItemDTO>? Items { get; set; }
	}
}

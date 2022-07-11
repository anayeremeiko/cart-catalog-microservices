using Catalog.Core.Entities;
using Catalog.SharedKernel.Interfaces;

namespace Catalog.Infrastructure.Entities
{
	public class ItemDTO : IAggregateRoot
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string ImageUrl { get; set; }

		public int CategoryId { get; set; }

		public virtual CategoryDTO Category { get; set; }

		public decimal Price { get; set; }

		public int Amount { get; set; }
	}
}

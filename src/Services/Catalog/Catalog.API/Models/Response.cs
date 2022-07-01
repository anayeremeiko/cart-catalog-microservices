using Catalog.SharedKernel.Interfaces;

namespace Catalog.API.Models
{
	public class Response<T> where T: IAggregateRoot
	{
		public T Data { get; set; }

		public IEnumerable<Link> Links { get; set; }
	}
}

using Catalog.API.Filters;

namespace Catalog.API.Services.Interfaces
{
	public interface IUriService
	{
		public Uri GetPageUri(PaginationFilter filter, string route);
	}
}

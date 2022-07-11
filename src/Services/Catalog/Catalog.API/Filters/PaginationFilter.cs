namespace Catalog.API.Filters
{
	public class PaginationFilter
	{
        /// <summary>
        /// Page number. Default value: 1
        /// </summary>
        public int? PageNumber { get; set; }

        /// <summary>
        /// Page size. Default value: 10
        /// </summary>
        public int? PageSize { get; set; }

        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize < 0 ? 10 : pageSize;
        }
    }
}

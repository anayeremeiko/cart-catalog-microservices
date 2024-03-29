﻿using Catalog.API.Filters;
using Catalog.API.Models;
using Catalog.API.Services.Interfaces;

namespace Catalog.API.Helpers
{
	public class PaginationHelper
	{
        public static PagedResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, PaginationFilter filter, int totalRecords, IUriService uriService, string route)
        {
            var respose = new PagedResponse<List<T>>(pagedData, filter.PageNumber.Value, filter.PageSize.Value);
            var totalPages = ((double)totalRecords / (double)filter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
				filter.PageNumber > 0 && filter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(filter.PageNumber.Value + 1, filter.PageSize.Value), route)
				: null;
            respose.PreviousPage =
                filter.PageNumber - 1 > 0 && filter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(filter.PageNumber.Value - 1, filter.PageSize.Value), route)
                : null;
            respose.FirstPage = uriService.GetPageUri(new PaginationFilter(1, filter.PageSize.Value), route);
            respose.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, filter.PageSize.Value), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;

            return respose;
        }
    }
}

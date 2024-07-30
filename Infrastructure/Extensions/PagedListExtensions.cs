using Common.Pagination;
using Microsoft.EntityFrameworkCore;
using Shared.Pagination;

namespace Infrastructure.Extensions;

public static class PageListExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> source,
        int totalCount,
        int? currentPageNumber = PaginationConstants.DefaultPageNumber,
        int? pageSize = PaginationConstants.DefaultPageSize,
        bool? ignorePagination = false
    ) where T : class
    {
        var sourceList = await source.ToListAsync();

        var pagedList = new PagedList<T>(sourceList, totalCount, currentPageNumber.Value!,
            pageSize <= 0 ? PaginationConstants.DefaultPageSize : pageSize.Value!);

        if (ignorePagination == true) pagedList.PageSize = totalCount;

        return pagedList;
    }

    public static PagedList<T> ToPagedList<T>(
        this IEnumerable<T> source,
        int totalCount,
        int currentPageNumber = PaginationConstants.DefaultPageNumber,
        int pageSize = PaginationConstants.DefaultPageSize,
        bool? ignorePagination = false
    ) where T : class
    {
        var sourceList = source.ToList();

        var pagedList = new PagedList<T>(sourceList, totalCount, currentPageNumber,
            pageSize <= 0 ? PaginationConstants.DefaultPageSize : pageSize);

        if (ignorePagination == true) pagedList.PageSize = totalCount;

        return pagedList;
    }
}
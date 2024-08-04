namespace Infrastructure.Extensions;

public static class PaginationExtensions
{
    public static IQueryable<T> Page<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
        => queryable
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
}
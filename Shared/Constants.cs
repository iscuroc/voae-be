namespace Shared;

public static class Constants
{
    public static class Pagination
    {
        public const int DefaultPageNumber = 1;
        public const int DefaultPageSize = 20;
    }
}

public record PaginationBase
{
    public int PageNumber { get; set; } = Constants.Pagination.DefaultPageNumber;
    public int PageSize { get; set; } = Constants.Pagination.DefaultPageSize;
}
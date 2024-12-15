namespace BankingSystemReporting.Common.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, int pageNumber, int pageSize)
            => query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }
}

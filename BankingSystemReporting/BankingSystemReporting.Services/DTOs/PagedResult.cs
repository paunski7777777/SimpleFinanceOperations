namespace BankingSystemReporting.Services.DTOs
{
    public class PagedResult<T>(IEnumerable<T> items, int totalItemsCount, int pageNumber, int pageSize)
    {
        public IEnumerable<T> Items { get; set; } = items;
        public int TotalItemsCount { get; } = totalItemsCount;
        public int PageNumber { get; } = pageNumber;
        public int PageSize { get; } = pageSize;
        public int TotalPages => (int)Math.Ceiling((double)this.TotalItemsCount / this.PageSize);
    }
}

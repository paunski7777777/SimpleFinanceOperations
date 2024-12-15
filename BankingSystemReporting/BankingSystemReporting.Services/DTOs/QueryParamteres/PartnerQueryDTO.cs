namespace BankingSystemReporting.Services.DTOs.QueryParamteres
{
    using BankingSystemReporting.Common.Constants;

    public class PartnerQueryDTO
    {
        public string? Name { get; set; }
        public int PageNumber { get; set; } = AppConstants.Pagination.DefaultPageNumber;
        public int PageSize { get; set; } = AppConstants.Pagination.DefaultPageSize;
    }
}

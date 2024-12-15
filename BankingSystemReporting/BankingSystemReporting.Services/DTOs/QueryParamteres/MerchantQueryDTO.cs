namespace BankingSystemReporting.Services.DTOs.QueryParamteres
{
    using BankingSystemReporting.Common.Constants;

    public class MerchantQueryDTO
    {
        public string? Name { get; set; }
        public string? BoardingDate { get; set; }
        public string? URL { get; set; }
        public string? Country { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? PartnerName { get; set; }
        public int PageNumber { get; set; } = AppConstants.Pagination.DefaultPageNumber;
        public int PageSize { get; set; } = AppConstants.Pagination.DefaultPageSize;
    }
}

namespace BankingSystemReporting.Services.DTOs.QueryParamteres
{
    using BankingSystemReporting.Common.Constants;

    public class TransactionQueryDTO
    {
        public string? CreateDate { get; set; }
        public string? Direction { get; set; }
        public decimal? Amount { get; set; }
        public string? Currency { get; set; }
        public string? DebtorIBAN { get; set; }
        public string? BeneficiaryIBAN { get; set; }
        public string? Status { get; set; }
        public string? ExternalId { get; set; }
        public string? MerchantName { get; set; }
        public int PageNumber { get; set; } = AppConstants.Pagination.DefaultPageNumber;
        public int PageSize { get; set; } = AppConstants.Pagination.DefaultPageSize;
    }
}

namespace BankingSystemReporting.Services.DTOs.Export
{
    using BankingSystemReporting.Models;
    using BankingSystemReporting.Services.Mapping;

    public class TransactionDTO : IMapFrom<Transaction>
    {
        public DateTimeOffset CreateDate { get; set; }
        public string Direction { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string DebtorIBAN { get; set; }
        public string BeneficiaryIBAN { get; set; }
        public string Status { get; set; }
        public string ExternalId { get; set; }
        public string MerchantName { get; set; }
    }
}

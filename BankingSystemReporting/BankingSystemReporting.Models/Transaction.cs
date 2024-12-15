namespace BankingSystemReporting.Models
{
    using BankingSystemReporting.Models.Contracts;
    using BankingSystemReporting.Models.Enums;

    using System.ComponentModel.DataAnnotations;

    public class Transaction : BaseEntity<int>
    {
        [Required]
        public DateTimeOffset CreateDate { get; set; }

        [Required]
        public TransactionDirection Direction { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public string DebtorIBAN { get; set; }

        [Required]
        public string BeneficiaryIBAN { get; set; }

        [Required]
        public TransactionStatus Status { get; set; }

        [Required]
        public string ExternalId { get; set; }

        [Required]
        public int MerchantID { get; set; }
        public Merchant Merchant { get; set; }
    }
}

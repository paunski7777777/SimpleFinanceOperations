namespace BankingSystemReporting.Models
{
    using BankingSystemReporting.Models.Contracts;

    using System.ComponentModel.DataAnnotations;

    public class Merchant : BaseEntity<int>, IBankUser
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTimeOffset BoardingDate { get; set; } = DateTimeOffset.Now;

        public string? URL { get; set; }
        public string? Country { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }

        [Required]
        public int PartnerID { get; set; }
        public Partner Partner { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = [];
    }
}

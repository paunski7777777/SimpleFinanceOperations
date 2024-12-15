namespace BankingSystemReporting.Models
{
    using BankingSystemReporting.Models.Contracts;

    using System.ComponentModel.DataAnnotations;

    public class Partner : BaseEntity<int>, IBankUser
    {
        [Required]
        public string Name { get; set; }

        public ICollection<Merchant> Merchants { get; set; } = [];
    }
}

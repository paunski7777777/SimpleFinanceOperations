namespace BankingSystemReporting.Models.Contracts
{
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}

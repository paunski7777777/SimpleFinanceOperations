namespace BankingSystemReporting.Services.DTOs.Export
{
    using BankingSystemReporting.Models;
    using BankingSystemReporting.Services.Mapping;

    public class MerchantDTO : IMapFrom<Merchant>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset BoardingDate { get; set; }
        public string? URL { get; set; }
        public string? Country { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string PartnerName { get; set; }
    }
}

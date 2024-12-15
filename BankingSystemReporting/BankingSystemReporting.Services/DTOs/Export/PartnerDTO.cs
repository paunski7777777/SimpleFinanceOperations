namespace BankingSystemReporting.Services.DTOs.Export
{
    using BankingSystemReporting.Models;
    using BankingSystemReporting.Services.Mapping;

    public class PartnerDTO : IMapFrom<Partner>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

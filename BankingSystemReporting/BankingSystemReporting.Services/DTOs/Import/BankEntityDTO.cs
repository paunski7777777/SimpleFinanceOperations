namespace BankingSystemReporting.Services.DTOs.Import
{
    using AutoMapper;

    using BankingSystemReporting.Models;
    using BankingSystemReporting.Services.Mapping;

    using System.Xml.Serialization;

    public class BankEntityDTO : IMapTo<Partner>, IMapTo<Merchant>, IHaveCustomMappings
    {
        [XmlElement(nameof(BankName))]
        public string BankName { get; set; }

        [XmlElement(nameof(BIC))]
        public string BIC { get; set; }

        [XmlElement(nameof(IBAN))]
        public string IBAN { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<BankEntityDTO, Partner>()
                .ForMember(m => m.Name, opt => opt.MapFrom(src => src.BankName));

            configuration
                .CreateMap<BankEntityDTO, Merchant>()
                .ForMember(m => m.Name, opt => opt.MapFrom(src => src.BankName));
        }
    }
}

namespace BankingSystemReporting.Services.DTOs.Import
{
    using AutoMapper;

    using BankingSystemReporting.Common.Constants;
    using BankingSystemReporting.Models;
    using BankingSystemReporting.Models.Enums;
    using BankingSystemReporting.Services.Mapping;

    using System.Xml.Serialization;

    public class TransactionDTO : IMapTo<Transaction>, IHaveCustomMappings
    {
        [XmlElement(nameof(ExternalId))]
        public string ExternalId { get; set; }

        [XmlElement(nameof(CreateDate))]
        public DateTime CreateDate { get; set; }

        [XmlElement(nameof(Amount))]
        public AmountDTO Amount { get; set; }

        [XmlElement(nameof(Status))]
        public string Status { get; set; }

        [XmlElement(nameof(Debtor))]
        public BankEntityDTO Debtor { get; set; }

        [XmlElement(nameof(Beneficiary))]
        public BankEntityDTO Beneficiary { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<TransactionDTO, Transaction>()
                .ForMember(m => m.Amount, opt => opt.MapFrom(src => src.Amount.Value))
                .ForMember(m => m.Currency, opt => opt.MapFrom(src => src.Amount.Currency))
                .ForMember(m => m.DebtorIBAN, opt => opt.MapFrom(src => src.Debtor.IBAN))
                .ForMember(m => m.BeneficiaryIBAN, opt => opt.MapFrom(src => src.Beneficiary.IBAN))
                .ForMember(m => m.Direction, opt => opt.MapFrom(src => MapDirection(src.Amount.Direction)))
                .ForMember(m => m.Status, opt => opt.MapFrom(src => Enum.Parse<TransactionStatus>(src.Status)));
        }

        private static TransactionDirection MapDirection(string direction)
        {
            return direction switch
            {
                AppConstants.XML.Directions.Debit => TransactionDirection.Debit,
                AppConstants.XML.Directions.Credit => TransactionDirection.Credit,
                _ => throw new ArgumentException(string.Format(AppConstants.Messages.Errors.InvalidDirectionValue, direction))
            };
        }

    }
}

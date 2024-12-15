namespace BankingSystemReporting.Services.DTOs.Import
{
    using System.Xml.Serialization;

    public class AmountDTO
    {
        [XmlElement(nameof(Direction))]
        public string Direction { get; set; }

        [XmlElement(nameof(Value))]
        public decimal Value { get; set; }

        [XmlElement(nameof(Currency))]
        public string Currency { get; set; }
    }
}

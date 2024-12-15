namespace BankingSystemReporting.Services.DTOs.Import
{
    using BankingSystemReporting.Common.Constants;

    using System.Xml.Serialization;


    [XmlRoot(AppConstants.XML.Attributes.Operation)]
    public class OperationDTO
    {
        [XmlElement(nameof(FileDate))]
        public DateTime FileDate { get; set; }

        [XmlArray(nameof(Transactions))]
        [XmlArrayItem(AppConstants.XML.Attributes.Transaction)]
        public TransactionDTO[] Transactions { get; set; }
    }
}

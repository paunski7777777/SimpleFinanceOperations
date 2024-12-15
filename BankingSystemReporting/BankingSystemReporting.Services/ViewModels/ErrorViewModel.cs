namespace BankingSystemReporting.Services.ViewModels
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrWhiteSpace(this.RequestId);
    }
}

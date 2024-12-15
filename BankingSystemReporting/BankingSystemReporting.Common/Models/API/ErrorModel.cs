namespace BankingSystemReporting.Common.Models.API
{
    using System.Net;

    public class ErrorModel
    {
        public HttpStatusCode Status { get; set; }
        public object? Errors { get; set; }

        public ErrorModel(HttpStatusCode status)
        {
            this.Status = status;
        }

        public ErrorModel(HttpStatusCode status, object errors)
        {
            this.Status = status;
            this.Errors = errors;
        }
    }
}

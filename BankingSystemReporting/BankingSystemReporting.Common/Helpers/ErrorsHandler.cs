namespace BankingSystemReporting.Common.Helpers
{
    using Microsoft.Extensions.Logging;

    using BankingSystemReporting.Common.Constants;
    using BankingSystemReporting.Common.Models.API;

    using System.Net;

    public static class ErrorsHandler
    {
        public static void HandleInvalidOperationError(ILogger logger, string serviceName, string methodName, string exceptionMessage)
        {
            string errorMessage = string.Format(AppConstants.Messages.Errors.GeneralError, serviceName, methodName, exceptionMessage);
            logger.LogError(errorMessage);
            throw new InvalidOperationException(errorMessage);
        }

        public static Result HandleResultError(ILogger logger, HttpStatusCode statusCode, string exceptionMessage, string serviceName, string methodName)
        {
            string errorMessage = string.Format(AppConstants.Messages.Errors.GeneralError, serviceName, methodName, exceptionMessage);
            logger.LogError(errorMessage);
            return new ErrorModel(statusCode, errorMessage);
        }

        public static void HandleArgumentNullException(ILogger logger, string serviceName, string methodName, string exceptionMessage)
        {
            string errorMessage = string.Format(AppConstants.Messages.Errors.GeneralError, serviceName, methodName, exceptionMessage);
            logger.LogError(errorMessage);
            throw new ArgumentNullException(errorMessage);
        }

        public static void HandleResultError(List<Result> results, ILogger logger, HttpStatusCode statusCode, string errorMessage)
        {   
            logger.LogError(errorMessage);
            results.Add(new ErrorModel(statusCode, errorMessage));
        }
    }
}

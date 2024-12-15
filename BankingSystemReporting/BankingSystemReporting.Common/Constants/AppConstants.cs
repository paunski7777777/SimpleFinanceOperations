namespace BankingSystemReporting.Common.Constants
{
    public static class AppConstants
    {
        public const char ErrorSeparator = ';';

        public static class FileFormats
        {
            public const string XML = "application/xml";
            public const string CSV = "text/csv";
        }

        public static class CSV
        {
            public static class FileNames
            {
                public const string Partners = "partners.csv";
                public const string Merchants = "merchants.csv";
                public const string Transactions = "transactions.csv";
            }

            public const bool HasHeaderRecord = true;
            public const string Delimeter = ";";
        }

        public static class XML
        {
            public static class Attributes
            {
                public const string Operation = nameof(Operation);
                public const string Transaction = nameof(Transaction);
            }

            public static class Directions
            {
                public const string Debit = "D";
                public const string Credit = "C";
            }
        }

        public static class Configurations
        {
            public static class Database
            {
                public const string ConnectionString = "DefaultConnection";
                public const int CommandTimeout = 3600;
            }
        }

        public static class Pagination
        {
            public const int DefaultPageNumber = 1;
            public const int DefaultPageSize = 10;
        }

        public static class Messages
        {
            public static class Errors
            {
                public const string InvalidXMLData = "Invalid XML data";
                public const string InvalidDirectionValue = "Invalid direction value: {0}";
                public const string ErrorProcessingTransaction = "Error processing transaction with external ID '{0}': {1}";
                public const string EmptyParameter = "Parameter '{0}' is empty";
                public const string GeneralError = "Error in {0}.{1}: {2}";
                public const string NoDataForExport = "No data for export";
                public const string EntityByIDNotFound = "Entity '{0}' with ID '{1}' not found";
            }
        }
    }
}

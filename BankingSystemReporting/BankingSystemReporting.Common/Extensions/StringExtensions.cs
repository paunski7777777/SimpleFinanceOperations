namespace BankingSystemReporting.Common.Extensions
{
    public static class StringExtensions
    {
        public static string NormalizeForComparison(this string? input)
        {
            return input?.Trim().ToLower() ?? string.Empty;
        }
    }
}

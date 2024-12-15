namespace BankingSystemReporting.Common.Models.API
{
    public class Result
    {
        public bool Succeeded { get; private set; }
        public ResultModel? Data { get; private set; }
        public ErrorModel? Error { get; private set; }

        public static implicit operator Result(bool succeeded)
            => new() { Succeeded = succeeded };

        public static implicit operator Result(ResultModel data)
          => new() { Succeeded = true, Data = data };

        public static implicit operator Result(ErrorModel errors)
            => new() { Error = errors };
    }
}

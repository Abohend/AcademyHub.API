namespace AcademyHub.Application.Common
{
    /// <summary>
    /// Encapsulates the result of an operation, including success/failure status and value.
    /// </summary>
    /// <typeparam name="T">The type of the result value.</typeparam>
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public string? Error { get; }
        public int StatusCode { get; }

        protected Result(bool isSuccess, T? value, string? error, int statusCode)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
            StatusCode = statusCode;
        }

        public static Result<T> Success(T value, int statusCode = 200) 
            => new(true, value, null, statusCode);

        public static Result<T> Failure(string error, int statusCode = 400) 
            => new(false, default, error, statusCode);
    }

    /// <summary>
    /// Non-generic Result for operations without a return value.
    /// </summary>
    public class Result : Result<object>
    {
        private Result(bool isSuccess, string? error, int statusCode) 
            : base(isSuccess, null!, error, statusCode) { }

        public static Result Success(int statusCode = 200) 
            => new(true, null, statusCode);

        public static new Result Failure(string error, int statusCode = 400) 
            => new(false, error, statusCode);
    }
}

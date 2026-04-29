namespace AcademyHub.Application.Common
{
    public class PaginatedResult<T> : Result<T>
    {
        public int TotalCount { get; }
        public int PageNumber { get; }
        public int PageSize { get; }

        private PaginatedResult(T value, int totalCount, int pageNumber, int pageSize, int statusCode = 200)
            : base(true, value, null, statusCode)
        {
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        private PaginatedResult(bool isSuccess, T? value, string? error, int statusCode)
            : base(isSuccess, value, error, statusCode)
        {
        }

        public static PaginatedResult<T> Success(T value, int totalCount, int pageNumber, int pageSize, int statusCode = 200)
            => new PaginatedResult<T>(value, totalCount, pageNumber, pageSize, statusCode);

        public static new PaginatedResult<T> Failure(string error, int statusCode = 400)
            => new PaginatedResult<T>(false, default, error, statusCode);
    }
}

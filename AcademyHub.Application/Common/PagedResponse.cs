using System.Collections.Generic;

namespace AcademyHub.Application.Common
{
    public class PagedResponse<T> : ApiResponse<T>
    {
        public PaginationMetadata? Pagination { get; set; }

        public static PagedResponse<T> Create(T data, int totalCount, int currentPage, int pageSize, string message = "Success", int statusCode = 200)
        {
            return new PagedResponse<T>
            {
                Success = true,
                StatusCode = statusCode,
                Message = message,
                Data = data,
                Pagination = new PaginationMetadata(totalCount, currentPage, pageSize)
            };
        }

        public new static PagedResponse<T> ErrorResponse(List<string> errors, string message = "Error", int statusCode = 400)
        {
            return new PagedResponse<T>
            {
                Success = false,
                StatusCode = statusCode,
                Message = message,
                Errors = errors
            };
        }
    }
}

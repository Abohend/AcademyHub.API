using System;
using System.Collections.Generic;

namespace AcademyHub.Application.Common
{
    /// <summary>
    /// Unified API response wrapper for all endpoints.
    /// </summary>
    /// <typeparam name="T">The type of the data payload.</typeparam>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static ApiResponse<T> SuccessResponse(T data, string message = "Success", int statusCode = 200)
        {
            return new ApiResponse<T>
            {
                Success = true,
                StatusCode = statusCode,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> ErrorResponse(List<string> errors, string message = "Error", int statusCode = 400)
        {
            return new ApiResponse<T>
            {
                Success = false,
                StatusCode = statusCode,
                Message = message,
                Errors = errors
            };
        }
    }

    /// <summary>
    /// Non-generic API response wrapper for error-only responses.
    /// </summary>
    public class ApiResponse : ApiResponse<object>
    {
        public static ApiResponse Error(List<string> errors, string message = "Error", int statusCode = 400)
        {
            return new ApiResponse
            {
                Success = false,
                StatusCode = statusCode,
                Message = message,
                Errors = errors
            };
        }
    }
}

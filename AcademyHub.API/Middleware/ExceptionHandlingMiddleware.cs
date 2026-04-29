using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using AcademyHub.Application.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AcademyHub.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = "An internal server error occurred.";
            var errors = new List<string>();

            switch (exception)
            {
                case KeyNotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    message = "Resource not found.";
                    break;
                case ArgumentException or FluentValidation.ValidationException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = "Validation error.";
                    errors.Add(exception.Message);
                    break;
                case InvalidOperationException:
                    statusCode = (int)HttpStatusCode.Conflict;
                    message = "Business rule violation.";
                    errors.Add(exception.Message);
                    break;
                default:
                    errors.Add(exception.Message);
                    break;
            }

            var response = ApiResponse.Error(errors, message, statusCode);
            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            return context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }
    }
}

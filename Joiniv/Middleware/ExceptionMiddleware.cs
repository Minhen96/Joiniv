using System.Net;
using System.Text.Json;
using Joiniv.Domain.Exceptions;

namespace Joiniv.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Let the request proceed
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex); // Catch any error!
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            // 1. Is it our custom ApiException?
            if (exception is ApiException apiException)
            {
                context.Response.StatusCode = apiException.StatusCode;
                var result = JsonSerializer.Serialize(new { message = apiException.Message });
                return context.Response.WriteAsync(result);
            }

            // 2. If it's a random crash, send a generic 500
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var genericResult = JsonSerializer.Serialize(new { message = "An internal server error occurred." });
            return context.Response.WriteAsync(genericResult);
        }
    }
}
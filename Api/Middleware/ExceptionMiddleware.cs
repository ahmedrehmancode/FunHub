using Application.Common;
using CEIS.Application.Common;
using CEIS.Domain.Exceptions;
using System.Text.Json;

namespace CEIS.Api.Middleware
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
                await _next(context); // Request aage bhejo
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning("Validation error: {Message}", ex.Message);
                await WriteResponseAsync(context, 422,
                    ApiResponse<object>.ValidationResponse(ex.Errors));
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning("Not found: {Message}", ex.Message);
                await WriteResponseAsync(context, 404,
                    ApiResponse<object>.NotFoundResponse(ex.Message));
            }
            catch (UnauthorizedException ex)
            {
                _logger.LogWarning("Unauthorized: {Message}", ex.Message);
                await WriteResponseAsync(context, 401,
                    ApiResponse<object>.UnauthorizedResponse(ex.Message));
            }
            catch (ForbiddenException ex)
            {
                _logger.LogWarning("Forbidden: {Message}", ex.Message);
                await WriteResponseAsync(context, 403,
                    ApiResponse<object>.ForbiddenResponse(ex.Message));
            }
            catch (ConflictException ex)
            {
                _logger.LogWarning("Conflict: {Message}", ex.Message);
                await WriteResponseAsync(context, 409,
                    ApiResponse<object>.ConflictResponse(ex.Message));
            }
            catch (AppException ex)
            {
                // Baaki sab custom exceptions yahan aayengi
                _logger.LogWarning("App error: {Message}", ex.Message);
                await WriteResponseAsync(context, ex.StatusCode,
                    ApiResponse<object>.FailResponse(ex.Message));
            }
            catch (Exception ex)
            {
                //Unexpected crash — production mein details mat dikhaao!
                _logger.LogError(ex, "Unexpected error occurred.");
                await WriteResponseAsync(context, 500,
                    ApiResponse<object>.ServerErrorResponse());
                _logger.LogError(ex, ex.ToString());

            }
        }

        private static async Task WriteResponseAsync<T>(
            HttpContext context,
            int statusCode,
            ApiResponse<T> response)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }

    }
}

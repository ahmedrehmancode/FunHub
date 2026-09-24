using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new();

        //200 - Success with data
        public static ApiResponse<T> SuccessResponse(T? data, string? message = "Success")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message ?? "Success",
                Data = data,
                Errors = new List<string>()
            };
        }

        //201 - Created
        public static ApiResponse<T> CreatedResponse(T data, string message = "Created Successfully")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                Errors = new List<string>()
            };
        }

        public static ApiResponse<T> FailResponse(string? message = "Something went wrong")
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message ?? "Something went wrong",
                Data = default,
                Errors = new List<string>()
            };
        }
        public static ApiResponse<T> FailResponse(List<string> errors)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = "Something went wrong",
                Data = default,
                Errors = errors
            };
        }

        //  422 - Validation Failed
        public static ApiResponse<T> ValidationResponse(List<string> errors)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = "Validation Failed",
                Data = default,
                Errors = errors
            };
        }

        // 401 - Unauthorized
        public static ApiResponse<T> UnauthorizedResponse(string message = "You are not authorized.")
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default,
                Errors = new List<string>()
            };
        }

        // 403 - Forbidden
        public static ApiResponse<T> ForbiddenResponse(string message = "You do not have permission.")
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default,
                Errors = new List<string>()
            };
        }

        // 404 - Not Found
        public static ApiResponse<T> NotFoundResponse(string message = "Resource not found.")
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default,
                Errors = new List<string>()
            };
        }

        public static ApiResponse<T> NotFoundResponse(List<string> errors)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = "Something went wrong",
                Data = default,
                Errors = errors
            };
        }

        // 409 - Conflict
        public static ApiResponse<T> ConflictResponse(string message = "Conflict occurred.")
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default,
                Errors = new List<string>()
            };
        }

        // 500 - Server Error
        public static ApiResponse<T> ServerErrorResponse(string message = "Internal server error.")
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default,
                Errors = new List<string>()
            };
        }
    }
}

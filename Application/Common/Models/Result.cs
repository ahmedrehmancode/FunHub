using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CEIS.Application.Common.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public string? Message { get; private set; }
        public T? Data { get; private set; }
        public List<string> Errors { get; private set; } = new();

        private Result(bool isSuccess, T? data, List<string>? errors, string? msg)
        {
            IsSuccess = isSuccess;
            Data = data;
            Errors = errors ?? new List<string>();
            Message = msg;
        }

        public static Result<T> Success(T data) => new(true, data, null, null);
        public static Result<T> Success(T data, string msg) => new(true, data, null, msg);
        public static Result<T> Failure(List<string> errors) => new(false, default, errors, null);
        public static Result<T> Failure(string error) => new(false, default, new List<string> { error }, null);
    }
}

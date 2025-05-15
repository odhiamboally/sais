
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DP.Application.Dtos.Common;

public partial record ApiResponse<T>
{
    public bool Successful { get; init; }
    public string? Message { get; init; }
    public T? Data { get; init; }
    public Exception? Exception { get; init; }
    public Dictionary<string, object>? AdditionalData { get; init; }

    [JsonConstructor]
    private ApiResponse(bool successful, string? message, T? data, Exception? exception, Dictionary<string, object>? additionalData)
    {
        Successful = successful;
        Message = message ?? "Operation Successful";
        Data = data;
        Exception = exception;
        AdditionalData = additionalData ?? new Dictionary<string, object>();
    }

    public static ApiResponse<T> Success(string message, T value, Exception? exception = null, Dictionary<string, object>? additionalData = null)
    {
        return new ApiResponse<T>(true, message, value, exception, additionalData);
    }

    public static ApiResponse<T> Failure(string errorMessage, T? data = default, Exception? error = null, Dictionary<string, object>? additionalData = null)
    {
        return new ApiResponse<T>(false, errorMessage, data, error, additionalData);
    }
}

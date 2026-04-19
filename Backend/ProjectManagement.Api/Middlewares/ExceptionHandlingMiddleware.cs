using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProjectManagement.Api.Wrappers;

namespace ProjectManagement.Api.Middlewares
{
    /// <summary>
    /// طبقة وسيطة (Middleware) لالتقاط الأخطاء غير المتوقعة محلياً وعالمياً وإرجاع استجابة موحدة للعميل.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        #region Fields
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        #endregion

        #region Constructor
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        #endregion

        #region Middleware Execution
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "حدث خطأ غير متوقع في النظام: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }
        #endregion

        #region Exception Handling Logic
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ApiResponse<object>(
                new[] { "حدث خطأ داخلي في الخادم. الرجاء مراجعة سجلات النظام لمزيد من التفاصيل." },
                "خطأ غير متوقع"
            );

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return context.Response.WriteAsync(jsonResponse);
        }
        #endregion
    }
}

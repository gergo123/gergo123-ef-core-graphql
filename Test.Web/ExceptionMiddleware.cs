using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Test.Web
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var guid = Guid.NewGuid().ToString();
                var s = ex.StackTrace;
                int st = s.LastIndexOf("line");
                _logger.LogError($"Something went wrong: {ex}\r\nGuid: {guid}\r\nLine number:{st.ToString()}");

                await HandleExceptionAsync(httpContext, ex, guid);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, string guid)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                statusCode = context.Response.StatusCode,
                message = guid + "\r\n" + exception.Message
            }));
        }
    }
}

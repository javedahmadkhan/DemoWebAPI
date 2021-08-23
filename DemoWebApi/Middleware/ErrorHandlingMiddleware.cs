using Demo.WebAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Demo.WebAPI.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            string result;

            if (exception is PresentationException)
            {
                code = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(new { error = exception.Message });
            }
            else
            {
                code = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(new { error = exception.Message });
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}

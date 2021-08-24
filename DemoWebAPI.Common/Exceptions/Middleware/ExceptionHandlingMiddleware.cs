namespace Demo.Common.Exceptions.Middleware
{
    using Demo.Common.Exceptions.Extensions;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System;
    using System.Net;
    using System.Threading.Tasks;

    public class ExceptionHandlingMiddleware
    {
        private RequestDelegate requestDelegate;

        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            var statusCode = HttpStatusCode.InternalServerError; // 500 if unexpected
            var result = string.Empty;

            switch (e)
            {
                case ModelNotFoundException modelNotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    result = JsonConvert.SerializeObject(modelNotFoundException.Message);
                    break;
                case ModelNotValidException modelNotValidException:
                    statusCode = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(modelNotValidException.Message);
                    break;
                default:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
            }

            if (result == string.Empty)
            {
                result = JsonConvert.SerializeObject(new { error = e.Message });
            }

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(result);
        }
    }
}

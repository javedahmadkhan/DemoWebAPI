namespace Demo.Common.Exceptions.Middleware
{
    using Demo.Common.Exceptions.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using static Demo.Common.Exceptions.Middleware.ExceptionHandlingMiddleware;

    /// <summary>
    /// 
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private RequestDelegate requestDelegate;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestDelegate"></param>
        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="e"></param>
        /// <returns></returns>
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

            if (string.IsNullOrEmpty(result))
            {
                result = JsonConvert.SerializeObject(new { error = e.Message });
            }

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(result);
        }

        public class CustomError
        {
            public int StatusCode { get; set; }
            
            public string Message { get; set; }

            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }
    }

    public static class CustomExcepExceptionExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }

        public static void CustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        Console.WriteLine($"Null exception logged: {contextFeature.Error.Message}");

                        await context.Response.WriteAsync(new CustomError()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }
    }
}

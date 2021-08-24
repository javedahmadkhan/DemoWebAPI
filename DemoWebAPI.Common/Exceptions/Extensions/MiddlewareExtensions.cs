namespace Demo.Common.Exceptions.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Middleware;

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseNumberChecker(this IApplicationBuilder app)
        {
            return app.UseMiddleware<NumberCheckerMiddleware>();
        }

        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}

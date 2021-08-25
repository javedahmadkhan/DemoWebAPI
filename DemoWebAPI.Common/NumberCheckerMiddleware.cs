namespace Demo.Common
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// 
    /// </summary>
    public class NumberCheckerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public NumberCheckerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var value = context.Request.Query["value"].ToString();
            if (int.TryParse(value, out var intValue))
                await context.Response.WriteAsync($"You entered a number: {intValue}");
            else
            {
                context.Items["value"] = value;
                await _next(context);
            }
        }
    }
}

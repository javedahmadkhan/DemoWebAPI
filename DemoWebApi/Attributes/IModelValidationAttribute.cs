using Microsoft.AspNetCore.Mvc.Filters;

namespace Demo.WebAPI.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModelValidationAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void OnActionExecuting(ActionExecutingContext context);
    }
}

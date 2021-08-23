using Microsoft.AspNetCore.Mvc.Filters;

namespace Demo.WebAPI.Attributes
{
    public interface IModelValidationAttribute
    {
        void OnActionExecuting(ActionExecutingContext context);
    }
}

//
// Copyright:   Copyright (c) 
//
// Description: Model Validation Attribute Class
//
// Project: 
//
// Author:  Accenture
//
// Created Date:  
//
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Demo.WebAPI.Attributes
{
    /// <summary>
    /// This class is used for Model Validation Attribute
    /// </summary>
    public sealed class ModelValidationAttribute : ActionFilterAttribute, IModelValidationAttribute
    {
        /// <summary>
        /// On Action Executing Method
        /// </summary>
        /// <param name="context">Action Executing Context</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}

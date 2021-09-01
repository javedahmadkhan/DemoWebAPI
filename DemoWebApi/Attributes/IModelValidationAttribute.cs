//
// Copyright:   Copyright (c) 
//
// Description: Model Validation Attribute Interface
//
// Project: 
//
// Author:  Accenture
//
// Created Date:  
//
using Microsoft.AspNetCore.Mvc.Filters;

namespace Demo.WebAPI.Attributes
{
    /// <summary>
    /// This interface is used for Model Validation Attribute
    /// </summary>
    public interface IModelValidationAttribute
    {
        /// <summary>
        /// On Action Executing Method
        /// </summary>
        /// <param name="context">Action Executing Context</param>
        void OnActionExecuting(ActionExecutingContext context);
    }
}

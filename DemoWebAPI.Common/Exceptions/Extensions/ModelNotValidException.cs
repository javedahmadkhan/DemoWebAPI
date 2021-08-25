using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Common.Exceptions.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public class ModelNotValidException : ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public ModelNotValidException(ValidationResult result)
        : base($"{result.MemberNames} {result.ErrorMessage}")
        {

        }
    }
}

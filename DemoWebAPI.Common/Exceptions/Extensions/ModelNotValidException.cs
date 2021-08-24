using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Common.Exceptions.Extensions
{
    public class ModelNotValidException : ApplicationException
    {
        public ModelNotValidException(ValidationResult result)
        : base($"{result.MemberNames} {result.ErrorMessage}")
        {

        }
    }
}

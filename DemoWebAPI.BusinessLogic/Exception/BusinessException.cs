using System;
using System.Runtime.Serialization;

namespace Demo.Repository.Exception
{
    [Serializable]
    public class BusinessException : SystemException
    {
        public BusinessException()
        {
        }

        public BusinessException(string message) : base(message)
        {
        }

        public BusinessException(string message, SystemException innerException)
            : base(message, innerException)
        {
        }

        public BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

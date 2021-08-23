using System;
using System.Runtime.Serialization;

namespace Demo.Repository.Exception
{
    [Serializable]
    public class RepositoryException : SystemException
    {
        public RepositoryException()
        {
        }

        public RepositoryException(string message) : base(message)
        {
        }

        public RepositoryException(string message, SystemException innerException)
            : base(message, innerException)
        {
        }

        public RepositoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

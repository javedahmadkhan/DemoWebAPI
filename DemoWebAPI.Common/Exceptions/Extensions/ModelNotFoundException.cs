using System;

namespace Demo.Common.Exceptions.Extensions
{
    public class ModelNotFoundException : ApplicationException
    {
        public ModelNotFoundException(string name, string key)
        : base($"{name} ({key}) not found")
        {

        }
    }
}

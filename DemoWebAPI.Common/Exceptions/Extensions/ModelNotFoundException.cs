using System;

namespace Demo.Common.Exceptions.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public class ModelNotFoundException : ApplicationException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="key"></param>
        public ModelNotFoundException(string name, string key)
        : base($"{name} ({key}) not found")
        {

        }
    }
}

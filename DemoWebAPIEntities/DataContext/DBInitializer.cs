using Microsoft.EntityFrameworkCore;

namespace Demo.Entities.DataContext
{
    /// <summary>
    /// 
    /// </summary>
    public static class DBInitializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public static void Initialize(DemoDBContext context)
        {
            context.Database.Migrate();
            context.Database.EnsureCreated();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        private static void Seed(DemoDBContext context)
        {
            // Add seed initialize method
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Demo.Entities.DataContext
{
    public static class DBInitializer
    {
        public static void Initialize(DemoDBContext context)
        {
            context.Database.Migrate();
            context.Database.EnsureCreated();
        }

        private static void Seed(DemoDBContext context)
        {
            // Add seed initialize method
        }
    }
}

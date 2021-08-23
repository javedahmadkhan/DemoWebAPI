using Microsoft.EntityFrameworkCore;

namespace Demo.Entities.DataContext
{
    public class DemoDBContext : DbContext
    {
        public DemoDBContext()
        {
        }

        public DemoDBContext(DbContextOptions<DemoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TodoItem> TodoItem { get; set; }

        //To create using package manager console
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

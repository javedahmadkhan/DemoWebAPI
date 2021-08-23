using Demo.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoWebAPIDataAccess
{
    public class TodoContext : DbContext
    {
        public TodoContext()
        {
        }

        public TodoContext(DbContextOptions<TodoContext> options)
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

using Microsoft.EntityFrameworkCore;

namespace Demo.Entities.DataContext
{
    /// <summary>
    /// 
    /// </summary>
    public class DemoDBContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public DemoDBContext()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public DemoDBContext(DbContextOptions<DemoDBContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<TodoItem> TodoItem { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Demo.Entities.DataContext
{
    /// <summary>
    /// 
    /// </summary>
    public class ResilientTransaction
    {
        private readonly DbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        private ResilientTransaction(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ResilientTransaction New(DbContext context)
        {
            return new ResilientTransaction(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(Func<Task> action)
        {
            // Use of an EF Core resiliency strategy when using multiple DbContexts
            // within an explicit BeginTransaction():
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                await action();
                await transaction.CommitAsync();
            });
        }
    }
}

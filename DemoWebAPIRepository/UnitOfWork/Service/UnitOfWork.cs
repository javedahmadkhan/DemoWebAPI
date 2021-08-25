using Demo.Entities.DataContext;
using Demo.Repository.Contract;
using Demo.Repository.Service;
using Demo.Repository.UnitOfWork.Contract;
using System.Threading.Tasks;

namespace Demo.Repository.UnitOfWork.Service
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DemoDBContext context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(DemoDBContext context)
        {
            this.context = context;

            this.TodoItemRepository = new TodoItemRepository(context);
        }

        /// <summary>
        /// 
        /// </summary>
        public UnitOfWork()
        {
        }

        public ITodoItemRepository TodoItemRepository { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync()
        {
            // Use of an EF Core resiliency strategy when using multiple DbContexts
            // within an explicit BeginTransaction():
            await ResilientTransaction.New(context).ExecuteAsync(async () =>
            {
                // Achieving atomicity between original catalog database
                // operation and the IntegrationEventLog thanks to a local transaction
                await context.SaveChangesAsync();
            });
        }
    }
}

using Demo.Entities.DataContext;
using Demo.Repository.Contract;
using Demo.Repository.Service;
using Demo.Repository.UnitOfWork.Contract;
using System.Threading.Tasks;

namespace Demo.Repository.UnitOfWork.Service
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DemoDBContext context;

        public UnitOfWork(DemoDBContext context)
        {
            this.context = context;

            this.TodoItemRepository = new TodoItemRepository(context);
        }

        public UnitOfWork()
        {
        }

        public ITodoItemRepository TodoItemRepository { get; private set; }

        public void Dispose()
        {
        }

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

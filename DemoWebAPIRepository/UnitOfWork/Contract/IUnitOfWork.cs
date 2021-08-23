using Demo.Repository.Contract;
using System;
using System.Threading.Tasks;

namespace Demo.Repository.UnitOfWork.Contract
{
    public interface IUnitOfWork : IDisposable
    {
        ITodoItemRepository TodoItemRepository { get; }

        Task SaveChangesAsync();
    }
}

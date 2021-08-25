using Demo.Repository.Contract;
using System;
using System.Threading.Tasks;

namespace Demo.Repository.UnitOfWork.Contract
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        ITodoItemRepository TodoItemRepository { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}

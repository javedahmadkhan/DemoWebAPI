using Demo.Entities;
using Demo.Repository.Contract.DB;

namespace Demo.Repository.Contract
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITodoItemRepository : IDBRepository<TodoItem>
    {

    }
}

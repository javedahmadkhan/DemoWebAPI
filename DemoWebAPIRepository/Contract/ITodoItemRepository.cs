using Demo.Entities;
using Demo.Repository.Contract.DB;

namespace Demo.Repository.Contract
{
    public interface ITodoItemRepository : IDBRepository<TodoItem>
    {

    }
}

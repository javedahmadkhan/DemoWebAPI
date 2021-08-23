using Demo.Entities;
using Demo.Entities.DataContext;
using Demo.Repository.Contract;
using Demo.Repository.Service.DB;

namespace Demo.Repository.Service
{
    public class TodoItemRepository : BaseDBRepository<TodoItem>, ITodoItemRepository
    {
        private readonly DemoDBContext context;
        public TodoItemRepository(DemoDBContext context)
            : base(context)
        {
            this.context = context;
        }
    }
}

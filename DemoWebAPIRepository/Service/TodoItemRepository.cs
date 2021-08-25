using Demo.Entities;
using Demo.Entities.DataContext;
using Demo.Repository.Contract;
using Demo.Repository.Service.DB;

namespace Demo.Repository.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class TodoItemRepository : BaseDBRepository<TodoItem>, ITodoItemRepository
    {
        private readonly DemoDBContext context;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public TodoItemRepository(DemoDBContext context)
            : base(context)
        {
            this.context = context;
        }
    }
}

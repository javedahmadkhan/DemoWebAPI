using Demo.Models;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Contract
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITodoItemManagementService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<TodoItemListDTO> GetTodoItemList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TodoItemDTO> GetTodoItem(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="todoItemDTO"></param>
        /// <returns></returns>
        Task<int> CreateOrUpdateTodoItem(TodoItemCreateOrUpdateDTO todoItemDTO);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteTodoItem(int id);
    }
}

using Demo.Models;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Contract
{
    public interface ITodoItemManagementService
    {
        Task<TodoItemListDTO> GetTodoItemList();


        Task<TodoItemDTO> GetTodoItem(int id);


        Task<int> CreateOrUpdateTodoItem(TodoItemCreateOrUpdateDTO todoItemDTO);


        Task<int> DeleteTodoItem(int id);
    }
}

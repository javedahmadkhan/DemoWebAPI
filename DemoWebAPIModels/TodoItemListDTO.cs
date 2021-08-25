using System.Collections.Generic;

namespace Demo.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TodoItemListDTO
    {
        public TodoItemListDTO()
        {
            this.TodoItemList = new List<TodoItemDTO>();
        }

        public List<TodoItemDTO> TodoItemList { get; set; }
    }
}

using AutoMapper;
using Demo.BusinessLogic.Contract;
using Demo.Entities;
using Demo.Models;
using Demo.Repository.Exception;
using Demo.Repository.UnitOfWork.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Service
{
    public class TodoItemManagementService : ITodoItemManagementService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TodoItemManagementService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<int> CreateOrUpdateTodoItem(TodoItemCreateOrUpdateDTO todoItemDTO)
        {
            try
            {
                var todoItem = unitOfWork.TodoItemRepository.FirstOrDefault(a => a.PKId == todoItemDTO.Id);

                if (todoItem == null)
                {
                    todoItem = mapper.Map<TodoItemCreateOrUpdateDTO, TodoItem>(todoItemDTO);
                }
                else
                {
                    todoItem = mapper.Map(todoItemDTO, todoItem);
                }

                unitOfWork.TodoItemRepository.AddOrUpdate(todoItem);
                await unitOfWork.SaveChangesAsync();

                return todoItem.Id;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<int> DeleteTodoItem(int id)
        {
            try
            {
                unitOfWork.TodoItemRepository.Remove(id);

                await unitOfWork.SaveChangesAsync();

                return id;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<TodoItemDTO> GetTodoItem(int id)
        {
            try
            {
                var result = new TodoItemDTO();

                var response = await unitOfWork.TodoItemRepository.FirstOrDefaultAsync(a => a.PKId == id);
                if (response != null)
                {
                    mapper.Map(response, result);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<TodoItemListDTO> GetTodoItemList()
        {
            try
            {
                var result = (await unitOfWork.TodoItemRepository.GetAsync()).OrderBy(a => a.TaskName);

                var response = mapper.Map<List<TodoItem>, List<TodoItemDTO>>(result.ToList());

                var todo_item_dto = new TodoItemListDTO
                {

                    TodoItemList = response.ToList()
                };

                return todo_item_dto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}

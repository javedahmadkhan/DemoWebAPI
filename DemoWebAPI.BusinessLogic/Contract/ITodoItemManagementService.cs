//
// Copyright:   Copyright (c) 
//
// Description: Todo Item Management Service Interface
//
// Project: 
//
// Author:  Accenture
//
// Created Date:  
//
using Demo.Models;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Contract
{
    /// <summary>
    /// This interface is used to performing Todo Item related functionality.
    /// </summary>
    public interface ITodoItemManagementService
    {
        /// <summary>
        /// Get Todo Item List Method
        /// </summary>
        /// <returns>Todo Item List DTO</returns>
        Task<TodoItemListDTO> GetTodoItemList();

        /// <summary>
        /// Get Todo Item Method
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Todo Item DTO</returns>
        Task<TodoItemDTO> GetTodoItem(int id);

        /// <summary>
        /// Create or Update Todo Item Method
        /// </summary>
        /// <param name="todoItemDTO">Todo Item DTO</param>
        /// <returns>Int</returns>
        Task<int> CreateOrUpdateTodoItem(TodoItemCreateOrUpdateDTO todoItemDTO);

        /// <summary>
        /// Delete Todo Item Method
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Int</returns>
        Task<int> DeleteTodoItem(int id);
    }
}

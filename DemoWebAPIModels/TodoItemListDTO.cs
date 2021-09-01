//
// Copyright:   Copyright (c) 
//
// Description: Todo Item List DTO Class
//
// Project: 
//
// Author:  Accenture
//
// Created Date:  
//
using System.Collections.Generic;

namespace Demo.Models
{
    /// <summary>
    /// Todo Item List DTO 
    /// </summary>
    public class TodoItemListDTO
    {
        /// <summary>
        /// Constructor for Todo Item List DTO
        /// </summary>
        public TodoItemListDTO()
        {
            this.TodoItemList = new List<TodoItemDTO>();
        }

        public List<TodoItemDTO> TodoItemList { get; set; }
    }
}

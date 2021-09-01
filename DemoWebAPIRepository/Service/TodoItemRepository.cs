//
// Copyright:   Copyright (c) 
//
// Description: Todo Item Repository Class
//
// Project: 
//
// Author:  Accenture
//
// Created Date:  
//
using Demo.Entities;
using Demo.Entities.DataContext;
using Demo.Repository.Contract;
using Demo.Repository.Service.DB;

namespace Demo.Repository.Service
{
    /// <summary>
    /// This class is used for managing Todo Item Repository methods
    /// </summary>
    public class TodoItemRepository : BaseDBRepository<TodoItem>, ITodoItemRepository
    {
        private readonly DemoDBContext context;

        /// <summary>
        /// Constructor for Todo Item Repository
        /// </summary>
        /// <param name="context"></param>
        public TodoItemRepository(DemoDBContext context)
            : base(context)
        {
            this.context = context;
        }
    }
}

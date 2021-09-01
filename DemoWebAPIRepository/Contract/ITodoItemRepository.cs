//
// Copyright:   Copyright (c) 
//
// Description: Todo Item Repository Interface
//
// Project: 
//
// Author:  Accenture
//
// Created Date:  
//
using Demo.Entities;
using Demo.Repository.Contract.DB;

namespace Demo.Repository.Contract
{
    /// <summary>
    /// This interface is used for managing Todo Item Repository methods
    /// </summary>
    public interface ITodoItemRepository : IDBRepository<TodoItem>
    {

    }
}

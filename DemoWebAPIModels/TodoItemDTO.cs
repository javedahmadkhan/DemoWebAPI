//
// Copyright:   Copyright (c) 
//
// Description: Todo Item DTO Class
//
// Project: 
//
// Author:  Accenture
//
// Created Date:  
//
namespace Demo.Models
{
    /// <summary>
    /// Todo Item DTO
    /// </summary>
    public class TodoItemDTO
    {
        public int Id { get; set; }

        public string TaskName { get; set; }

        public string MorningTask { get; set; }

        public string AfternoonTask { get; set; }

        public string EveningTask { get; set; }

        public bool IsTaskComplete { get; set; }
    }
}

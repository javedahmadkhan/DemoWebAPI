//
// Copyright:   Copyright (c) 
//
// Description: Todo Item Create or Update DTO Class
//
// Project: 
//
// Author:  Accenture
//
// Created Date:  
//

using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    /// <summary>
    /// Todo Item Create or Update DTO
    /// </summary>
    public class TodoItemCreateOrUpdateDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Task Name is required")]
        [StringLength(150)]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "Morning Task is required")]
        [StringLength(150)]
        public string MorningTask { get; set; }

        [Required(ErrorMessage = "Afternoon Task is required")]
        [StringLength(150)]
        public string AfternoonTask { get; set; }

        [Required(ErrorMessage = "Evening Task is required")]
        [StringLength(150)]
        public string EveningTask { get; set; }

        [Display(Name = "Active")]
        public bool IsTaskComplete { get; set; }
    }
}

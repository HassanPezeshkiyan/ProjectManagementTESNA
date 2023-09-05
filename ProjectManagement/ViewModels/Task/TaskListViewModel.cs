﻿using DB.Entity;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.ViewModels
{
    public class TaskListViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public DateTime? TaskDeadline { get; set; }

        public List<TaskCategoryInfoViewModel>? TaskCategories { get; set; }
        public List<UserInfoViewModel>? TaskUsers { get; set; }
    }
}

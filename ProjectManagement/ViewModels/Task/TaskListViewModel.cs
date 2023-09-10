using DB.Entity;
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

        public bool? TaskStatus { get; set; }
        public string? TaskStatusTitle => this.TaskStatus == false ? "انجام نشده" : "انجام شده";

        public List<TaskCategoryInfoViewModel>? TaskCategories { get; set; }
        public List<UserInfoViewModel>? TaskUsers { get; set; }
        public List<UserTaskLogInfoViewModel>? TaskStatusLog { get; set; }
    }
}

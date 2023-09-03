using DB.Entity;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.ViewModels
{
    public class TaskCategoryInfoViewModel
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        public int CategoryId { get; set; }
        public string? CategroyTitle { get; set; }
    }
}

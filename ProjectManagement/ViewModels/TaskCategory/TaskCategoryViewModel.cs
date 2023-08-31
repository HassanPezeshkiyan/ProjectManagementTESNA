using DB.Entity;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.ViewModels
{
    public class TaskCategoryViewModel
    {
        
        public int Id { get; set; }
        [Required]
        public int TaskId { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string? CategroyTitle => this.Category.Title??"";
    }
}

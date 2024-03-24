using DB.Entity;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.ViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        //[Required]
        public string? Title { get; set; }
        //[Required]
        public string? Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? TaskDeadline { get; set; }
        
        public List<int>? TaskCategoryIds { get; set; }

        public List<int>? TaskUserIds { get; set; }
        public int? CreatorId { get; set; }
        public bool? TaskStatus { get; set; }

    }
}

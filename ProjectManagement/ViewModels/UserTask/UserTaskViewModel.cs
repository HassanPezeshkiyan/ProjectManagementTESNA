using DB.Entity;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.ViewModels
{
    public class UserTaskViewModel
    {
        
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        

        [Required]
        public int TaskId { get; set; }
      
        public bool? TaskStatus { get; set; }
        public string? TaskStatusTitle => this.TaskStatus == false ? "انجام نشده" : "انجام شده";

        public DateTime CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

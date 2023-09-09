using DB.Entity;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.ViewModels
{
    public class UserTaskViewModel
    {
        
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        
        public DateTime CreationDate { get; set; }
    }
}

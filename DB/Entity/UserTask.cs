using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entity
{
    public class UserTask
    {
        [Required]
        [Key]
        public int Id { get; set; }
        
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public int TaskId { get; set; }
        public ProjectTask Task{ get; set; }
        public bool TaskStatus { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

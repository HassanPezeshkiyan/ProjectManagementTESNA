using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entity
{
    public class ProjectTask
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public DateTime? TaskDeadline { get; set; }
        


        public ICollection<TaskCategory> TaskCategories{ get; set; }


    }
}

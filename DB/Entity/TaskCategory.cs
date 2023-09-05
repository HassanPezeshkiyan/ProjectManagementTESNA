using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entity
{
    public class TaskCategory
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public int TaskId { get; set; }
        public ProjectTask Task { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category{ get; set; }
    }
}

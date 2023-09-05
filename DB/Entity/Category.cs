using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entity
{
    public class Category
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description{ get; set; }

        /// <summary>
        /// creator of this category
        /// </summary>
        [Required]
        public int UserId { get; set; }
        public User Creator { get; set; }


        public ICollection<TaskCategory>? TaskCategories{ get; set; }
    }
}

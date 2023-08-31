using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// creator of this category
        /// </summary>
        [Required]
        public int UserId { get; set; }
        public string? UserFullName { get; set; }
    }
}

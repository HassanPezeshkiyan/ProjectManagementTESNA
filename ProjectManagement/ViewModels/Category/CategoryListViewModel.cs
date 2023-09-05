using DB.Entity;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.ViewModels
{
    public class CategoryListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// creator of this category
        /// </summary>
        public int UserId { get; set; }
        public UserInfoViewModel Creator{ get; set; }

    }
}

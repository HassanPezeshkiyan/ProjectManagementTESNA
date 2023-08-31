using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.ViewModels
{
    public class UserInfoViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? FullName => this.FirstName + " " + this.LastName; 
        public string UserName { get; set; }
    }
}

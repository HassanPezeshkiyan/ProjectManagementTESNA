using DB.Entity;

namespace ProjectManagement.ViewModels
{
    public class UserTaskLogInfoViewModel
    {
        public int Id { get; set; }
        public int UserTaskId { get; set; }
        public int FunctorId { get; set; }
        public UserInfoViewModel Functor { get; set; }

        public DateTime CreationDate { get; set; }
    }
}

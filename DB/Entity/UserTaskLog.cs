using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entity
{
    public class UserTaskLog
    {
        public int Id { get; set; }
        public int UserTaskId { get; set; }
        public UserTask UserTask{ get; set; }

        public int FunctorId { get; set; }
        public User Functor{ get; set; }

        public DateTime CreationDate { get; set; }
    }
}

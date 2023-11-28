using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using todoList.Entities.Base;

namespace todoList.Entities.Relations
{
    public class UsersTasks
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int TaskId { get; set; }
        public _Task Task { get; set; }
    }
}
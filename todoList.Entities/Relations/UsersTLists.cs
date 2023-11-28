using todoList.Entities.Base;

namespace todoList.Entities.Relations
{
    public class UsersTLists
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int TListId { get; set; }
        public TList TList { get; set; }
    }
}
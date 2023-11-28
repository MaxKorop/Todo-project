using System;

namespace todoList.DTOS.Responses
{
    public class ViewTaskResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string StatusName { get; set; }
        public string PriorityName { get; set; }
        public int CreatedById { get; set; }
    }
}
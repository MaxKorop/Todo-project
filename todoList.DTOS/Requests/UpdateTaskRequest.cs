using System;

namespace todoList.DTOS.Requests
{
    public class UpdateTaskRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int CreatedById { get; set; }
        public int TListId { get; set; }
        public int? StatusId { get; set; }
        public int? PriorityId { get; set; }
    }
}
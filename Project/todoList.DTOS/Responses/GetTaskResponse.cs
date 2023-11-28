using System;

namespace todoList.DTOS.Responses
{
    public class GetTaskResponse
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
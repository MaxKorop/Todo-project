using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace todoList.Entities.Base
{
    public class Status : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<_Task> Tasks { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace todoList.Entities.Base
{
    public class Priority : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<_Task> Tasks { get; set; }
    }
}
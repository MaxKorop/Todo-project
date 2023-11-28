using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using todoList.Entities.Relations;

namespace todoList.Entities.Base
{
    public class TList : IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedById { get; set; }
        public User CreatedBy { get; set; }
        public ICollection<_Task> Tasks { get; set; }

        public ICollection<UsersTLists> Users;
    }
}
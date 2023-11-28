using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using todoList.Entities.Relations;

namespace todoList.Entities.Base
{
    public class _Task : IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        [Required]
        
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }


        [ForeignKey(nameof(TListId))]
        public TList TList { get; set; }

        [ForeignKey(nameof(PriorityId))]
        public Priority Priority { get; set; }

        [ForeignKey(nameof(StatusId))]
        public Status Status { get; set; }

        [Required]
        [ForeignKey(nameof(CreatedById))]
        public User CreatedBy { get; set; }


        public int? TListId { get; set; }
        [Required]
        public int? CreatedById { get; set; }
        public int? StatusId { get; set; }
        public int? PriorityId { get; set; }

        public bool IsActive { get; set; }


        public ICollection<UsersTasks> AssignedUsers { get; set; }
    }
}
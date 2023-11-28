using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using todoList.Entities.Relations;

namespace todoList.Entities.Base
{
    public class User : IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string About { get; set; }
        public DateTime? BirthDate { get; set; }
        public ICollection<_Task> CreatedTasks { get; set; }
        public ICollection<TList> CreatedLists { get; set; }
        public ICollection<UsersTasks> AssignedTasks { get; set; }
        public ICollection<UsersTLists> AssignedLists { get; set; }
        public string ImgUrl { get; set; }
    }
}
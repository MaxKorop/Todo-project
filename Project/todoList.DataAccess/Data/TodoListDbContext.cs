using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todoList.Entities.Base;
using todoList.Entities.Relations;

namespace todoList.DataAccess.Data
{
    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext(DbContextOptions<TodoListDbContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<TList> TaskLists { get; set; }
        public DbSet<_Task> Tasks { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<UsersTasks> UsersTasks { get; set; }
        public DbSet<UsersTLists> UsersLists { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// _Task
            modelBuilder.Entity<_Task>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<_Task>()
                .HasOne(task => task.Priority)
                .WithMany(priority => priority.Tasks)
                .HasForeignKey(task => task.PriorityId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<_Task>()
                .HasOne(task => task.Status)
                .WithMany(status => status.Tasks)
                .HasForeignKey(task => task.StatusId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<_Task>()
                .HasOne(task => task.TList)
                .WithMany(list => list.Tasks)
                .HasForeignKey(task => task.TListId)
                .OnDelete(DeleteBehavior.Cascade);

            //// User
            modelBuilder.Entity<User>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<User>()
                .HasMany(u => u.CreatedTasks)
                .WithOne(t => t.CreatedBy)
                .HasForeignKey(ct => ct.CreatedById)
                .OnDelete(DeleteBehavior.Cascade);

            //// TList
            modelBuilder.Entity<TList>()
                .HasKey(list => list.Id);

            modelBuilder.Entity<TList>()
                .HasOne(list => list.CreatedBy)
                .WithMany(user => user.CreatedLists)
                .HasForeignKey(list => list.CreatedById)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Status>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Priority>()
                .HasKey(p => p.Id);

            //// Relations
            modelBuilder.Entity<UsersTasks>()
                .HasKey(ut => new { ut.UserId, ut.TaskId });
            modelBuilder.Entity<UsersTasks>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UsersTasks>()
                .HasOne(ut => ut.Task)
                .WithMany(t => t.AssignedUsers)
                .HasForeignKey(ut => ut.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UsersTLists>()
                .HasKey(ul => new { ul.UserId, ul.TListId });
            modelBuilder.Entity<UsersTLists>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.AssignedLists)
                .HasForeignKey(ut => ut.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UsersTLists>()
                .HasOne(ut => ut.TList)
                .WithMany(t => t.Users)
                .HasForeignKey(ut => ut.TListId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

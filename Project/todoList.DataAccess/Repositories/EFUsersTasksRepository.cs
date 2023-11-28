using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todoList.DataAccess.Data;
using todoList.DataAccess.Repositories.Interfaces;
using todoList.Entities.Base;
using todoList.Entities.Relations;

namespace todoList.DataAccess.Repositories
{
    public class EFUsersTasksRepository : IUsersTasksRepository
    {
        private readonly TodoListDbContext context;

        public EFUsersTasksRepository(TodoListDbContext context)
        {
            this.context = context;
        }

        public async Task<IList<UsersTasks>> GetAllAsync()
        {
            return await context.UsersTasks.ToListAsync();
        }

        public async Task<IList<User>> GetUsersByTaskIdAsync(int taskId)
        {
            var users = new List<User>();
            var userTasks = await context.UsersTasks.ToListAsync();

            foreach (var userTask in userTasks
                         .Where(userTask => userTask.TaskId == taskId))
            {
                var task = await context.Users.FindAsync(userTask.UserId);
                users.Add(task);
            }

            return users;
        }

        public async Task<IList<_Task>> GetTasksByUserIdAsync(int userId)
        {
            var tasks = new List<_Task>();
            var userTasks = await context.UsersTasks.ToListAsync();

            foreach (var userTask in userTasks.Where(userTask => userTask.UserId == userId))
            {
                var task = await context.Tasks.FindAsync(userTask.TaskId);
                tasks.Add(task);
            }

            return tasks;
        }

        public async Task<bool> AddAsync(UsersTasks usersTasks)
        {
            var entity = await context.UsersTasks.AddAsync(usersTasks);
            await context.SaveChangesAsync();

            return entity != null ? true : false;
        }
        public async Task<bool> AddByIdsAsync(int taskId, int userId)
        {
            var ut = new UsersTasks() { TaskId = taskId, UserId = userId };

            var entity = await context.UsersTasks.AddAsync(ut);
            await context.SaveChangesAsync();

            return entity != null;
        }

        public async Task<int> DeleteAsync(int userId, int taskId)
        {
            int count = 0;

            var usersTasks = await context.UsersTasks.ToListAsync();

            foreach (var userTask in usersTasks.Where(userTask => userTask.UserId == userId && userTask.TaskId == taskId))
            {
                context.UsersTasks.Remove(userTask);
                count++;
            }
            await context.SaveChangesAsync();

            return count;
        }

        public async Task<int> DeleteUserAllAsync(int userId)
        {
            int count = 0;

            var usersTasks = await context.UsersTasks.ToListAsync();

            foreach (var userTask in usersTasks.Where(userTask => userTask.UserId == userId))
            {
                context.UsersTasks.Remove(userTask);
                count++;
            }
            await context.SaveChangesAsync();

            return count;
        }

        public async Task<int> DeleteTaskAllAsync(int taskId)
        {
            int count = 0;

            var usersTasks = await context.UsersTasks.ToListAsync();

            foreach (var userTask in usersTasks.Where(userTask => userTask.TaskId == taskId))
            {
                context.UsersTasks.Remove(userTask);
                count++;
            }
            await context.SaveChangesAsync();

            return count;
        }
    }
}
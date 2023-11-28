using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todoList.DataAccess.Data;
using todoList.DataAccess.Repositories.Interfaces;
using todoList.Entities.Base;

namespace todoList.DataAccess.Repositories
{
    public class EFTaskRepository : ITaskRepository
    {
        private readonly TodoListDbContext context;
        public EFTaskRepository(TodoListDbContext context) => this.context = context;

        public async Task<IList<_Task>> GetAllAsync()
            => await context.Tasks.ToListAsync();
        public async Task<_Task> GetAsync(int id)
            => await context.Tasks.FindAsync(id);
        public async Task<int> Add(_Task task)
        {
            task.CreatedDate = DateTime.Now;
            task.ModifiedDate = DateTime.Now;

            await context.Tasks.AddAsync(task);
            await context.SaveChangesAsync();

            return task.Id;
        }
        public async Task<int> Update(_Task task)
        {
            task.ModifiedDate = DateTime.Now;

            context.Tasks.Update(task);
            return await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var task = await context.Tasks.FirstOrDefaultAsync(task => task.Id == id);

            context.Tasks.Remove(task);
            await context.SaveChangesAsync();
        }

        public async Task<bool> IsExist(int id)
            => await context.Tasks.AnyAsync(task => task.Id == id);

        public async Task<IList<Priority>> GetAllPrioritiesAsync() => await context.Priorities.ToListAsync();
        public async Task<IList<Status>> GetAllStatusesAsync() => await context.Statuses.ToListAsync(); }
}
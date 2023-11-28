using System;
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
    public class EFListRepository : IListRepository
    {
        private readonly TodoListDbContext context;
        public EFListRepository(TodoListDbContext context) => this.context = context;
        public async Task<IList<TList>> GetAllAsync() => await context.TaskLists.ToListAsync();
        public async Task<TList> GetAsync(int id) => await context.TaskLists.FindAsync(id);
        public async Task<int> Add(TList taskList)
        {
            var user = await context.Users.FirstOrDefaultAsync(user => user.Id == taskList.CreatedById);

            taskList.CreatedDate = DateTime.Now;
            taskList.ModifiedDate = DateTime.Now;
            taskList.CreatedBy = user;

            var ul = new UsersTLists() { TList = taskList, TListId = taskList.Id, User = user };

            await context.TaskLists.AddAsync(taskList);
            await context.UsersLists.AddAsync(ul);
            await context.SaveChangesAsync();

            return taskList.Id;
        }

        public async Task<int> Update(TList taskList)
        {
            taskList.ModifiedDate = DateTime.Now;

            context.TaskLists.Update(taskList);
            return await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var taskList = await context.TaskLists.FirstOrDefaultAsync(taskList => taskList.Id == id);
            context.TaskLists.Remove(taskList);
            await context.SaveChangesAsync();
        }
        public async Task<bool> IsExist(int id) => await context.TaskLists.AnyAsync(taskList => taskList.Id == id);

        //public async Task<IList<TList>> GetAllListOfUserAsync(int id)
        //{
        //    var allLists = await context.TaskLists.ToListAsync();
        //    var filteredLists = allLists.Where(item => item.CreatedById == id).ToList();

        //    return filteredLists;
        //}
    }
}
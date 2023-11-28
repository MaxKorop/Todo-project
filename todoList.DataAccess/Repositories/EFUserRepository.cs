using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todoList.DataAccess.Data;
using todoList.DataAccess.Repositories.Interfaces;
using todoList.Entities.Base;

namespace todoList.DataAccess.Repositories
{
    public class EFUserRepository : IUserRepository
    {
        private readonly TodoListDbContext context;
        public EFUserRepository(TodoListDbContext context) => this.context = context;


        public async Task<IList<User>> GetAllAsync() => await context.Users.ToListAsync();
        public async Task<User> GetAsync(int id) => await context.Users.FindAsync(id);

        public async Task<int> Add(User user)
        {
            user.CreatedDate = DateTime.Now;
            user.ModifiedDate = DateTime.Now;

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<int> Update(User user)
        {
            user.ModifiedDate = DateTime.Now;

            context.Users.Update(user);
            return await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(t => t.Id == id);

            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }

        public async Task<bool> IsExist(int id) =>
            await context.Users.AnyAsync(user => user.Id == id);

        public async Task<User> ValidateUser(string userName) =>
            await context.Users.FirstOrDefaultAsync(user => user.UserName == userName);

        public async Task<bool> IsUserNameExist(string userName) =>
            await context.Users.AnyAsync(user => user.UserName == userName);

        public async Task<bool> IsEmailExist(string email) =>
            await context.Users.AnyAsync(user => user.Email == email);
    }
}

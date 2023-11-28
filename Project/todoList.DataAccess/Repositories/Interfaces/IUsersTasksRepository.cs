using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using todoList.Entities.Base;
using todoList.Entities.Relations;

namespace todoList.DataAccess.Repositories.Interfaces
{
    public interface IUsersTasksRepository
    {
        public Task<IList<UsersTasks>> GetAllAsync();
        public Task<IList<User>> GetUsersByTaskIdAsync(int taskId);
        public Task<IList<_Task>> GetTasksByUserIdAsync(int userId);
        public Task<bool> AddAsync(UsersTasks usersTasks);
        public Task<bool> AddByIdsAsync(int taskId, int userId);
        public Task<int> DeleteAsync(int userId, int taskId);
        public Task<int> DeleteUserAllAsync(int userId);
        public Task<int> DeleteTaskAllAsync(int taskId);
    }
}
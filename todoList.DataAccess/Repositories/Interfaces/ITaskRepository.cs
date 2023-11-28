using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using todoList.Entities.Base;

namespace todoList.DataAccess.Repositories.Interfaces
{
    public interface ITaskRepository : IRepository<_Task>
    {
        public Task<IList<Priority>> GetAllPrioritiesAsync();
        public Task<IList<Status>> GetAllStatusesAsync();
    }
}
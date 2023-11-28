using System.Collections.Generic;
using System.Threading.Tasks;
using todoList.Entities.Base;

namespace todoList.DataAccess.Repositories.Interfaces
{
    public interface IListRepository : IRepository<TList>
    {
        // public Task<IList<TList>> GetAllListOfUserAsync(int id);
    }
}
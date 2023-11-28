using System.Collections.Generic;
using System.Threading.Tasks;
using todoList.Entities.Base;

namespace todoList.Business.Interfaces
{
    public interface IListService
    {
        Task<int> AddList(TList list);
        Task<int> UpdateList(TList list);
        Task DeleteList(int id);
        Task<TList> GetListById(int id);
        Task<ICollection<TList>> GetListsAsync();
        Task<bool> IsExist(int id);
        Task<ICollection<TList>> GetAllListOfUserAsync(int userId);
    }
}
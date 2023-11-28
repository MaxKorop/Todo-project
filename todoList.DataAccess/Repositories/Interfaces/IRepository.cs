using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using todoList.Entities;

namespace todoList.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        Task<IList<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task<int> Add(T entity);
        Task<int> Update(T entity);
        Task DeleteAsync(int id);
        Task<bool> IsExist(int id);
    }
}
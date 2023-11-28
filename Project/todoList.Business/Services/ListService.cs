using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todoList.Business.Interfaces;
using todoList.DataAccess.Repositories.Interfaces;
using todoList.Entities.Base;

namespace todoList.Business.Services
{
    public class ListService : IListService
    {
        private readonly IListRepository listRepository;
        private readonly ITaskRepository taskRepository;

        public ListService(
            IListRepository listRepository,
            ITaskRepository taskRepository)
        {
            this.listRepository = listRepository;
            this.taskRepository = taskRepository;
        }

        public async Task<int> AddList(TList list) => await listRepository.Add(list);

        public async Task<int> UpdateList(TList list) => await listRepository.Update(list);
        public async Task DeleteList(int id)
        {
            await listRepository.DeleteAsync(id);

            var tasks = await taskRepository.GetAllAsync();
            var filteredTasks = tasks.ToList().Where(task => task.TListId == id);
            foreach (var task in filteredTasks)
            {
                await taskRepository.DeleteAsync(task.Id);
            }
        }

        public async Task<TList> GetListById(int id) => await listRepository.GetAsync(id);

        public async Task<ICollection<TList>> GetListsAsync() => await listRepository.GetAllAsync();

        public async Task<bool> IsExist(int id) => await listRepository.IsExist(id);

        public async Task<ICollection<TList>> GetAllListOfUserAsync(int userId)
        {
            var lists = await listRepository.GetAllAsync();
            var filteredLists = lists.Where(item => item.CreatedById == userId);

            return filteredLists.ToList();
        }
    }
}
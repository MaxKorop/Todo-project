using System.Collections.Generic;
using System.Threading.Tasks;
using todoList.DTOS.Requests;
using todoList.DTOS.Responses;
using todoList.Entities.Base;

namespace todoList.Business.Interfaces
{
    public interface ITaskService
    {
        Task<int> AddTask(CreateTaskRequest request);
        Task<int> UpdateTask(UpdateTaskRequest request);
        Task DeleteTask(int id);
        Task<GetTaskResponse> GetTaskById(int id);
        Task<ICollection<_Task>> GetTasksAsync();
        Task<bool> IsExist(int id);
        Task<ICollection<ViewTaskResponse>> GetTasksByUserId(int userId);
        Task<ICollection<ViewTaskResponse>> GetTasksByListId(int listId);
        Task<ICollection<Priority>> GetAllPrioritiesAsync();
        Task<ICollection<Status>> GetAllStatusesAsync();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using todoList.Business.Interfaces;
using todoList.DataAccess.Repositories.Interfaces;
using todoList.DTOS.Requests;
using todoList.DTOS.Responses;
using todoList.Entities.Base;
using todoList.Entities.Relations;

namespace todoList.Business.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository taskRepository;
        private readonly IUsersTasksRepository usersTasksRepository;
        private readonly IMapper mapper;

        public TaskService(
            ITaskRepository taskRepository,
            IUsersTasksRepository usersTasksRepository,
            IMapper mapper)
        {
            this.taskRepository = taskRepository;
            this.usersTasksRepository = usersTasksRepository;
            this.mapper = mapper;
        }

        public async Task<int> AddTask(CreateTaskRequest request)
        {
            var task = mapper.Map<_Task>(request);

            var taskId = await taskRepository.Add(task);
            await usersTasksRepository.AddByIdsAsync(taskId, (int)task.CreatedById);

            return taskId;
        }

        public async Task<int> UpdateTask(UpdateTaskRequest request)
        {
            var task = mapper.Map<_Task>(request);

            var taskId = await taskRepository.Update(task);
            return taskId;
        }

        public async Task DeleteTask(int id) => await taskRepository.DeleteAsync(id);
        public async Task<GetTaskResponse> GetTaskById(int id)
        {
            var task = await taskRepository.GetAsync(id);
            var response = mapper.Map<GetTaskResponse>(task);
            return response;
        }

        public async Task<ICollection<_Task>> GetTasksAsync() => await taskRepository.GetAllAsync();
        public async Task<bool> IsExist(int id) => await taskRepository.IsExist(id);

        public async Task<ICollection<ViewTaskResponse>> GetTasksByUserId(int userId)
        {
            var tasks = new List<ViewTaskResponse>();
            var priorities = await GetAllPrioritiesAsync();
            var statuses = await GetAllStatusesAsync();
            var requests = await usersTasksRepository.GetTasksByUserIdAsync(userId);

            foreach (var request in requests)
            {
                var task = mapper.Map<ViewTaskResponse>(request);

                task.PriorityName = priorities.FirstOrDefault(p => p.Id == request.PriorityId)?.Name;
                task.StatusName = statuses.FirstOrDefault(p => p.Id == request.StatusId)?.Name;

                tasks.Add(task);
            }
            return tasks;
        }

        public async Task<ICollection<ViewTaskResponse>> GetTasksByListId(int listId)
        {
            var responses = new List<ViewTaskResponse>();

            var priorities = await GetAllPrioritiesAsync();
            var statuses = await GetAllStatusesAsync();

            var allTasks = await taskRepository.GetAllAsync();
            var tasks = allTasks.Where(task => task.TListId == listId);

            foreach (var request in tasks)
            {
                var response = mapper.Map<ViewTaskResponse>(request);

                response.PriorityName = priorities.FirstOrDefault(p => p.Id == request.PriorityId)?.Name;
                response.StatusName = statuses.FirstOrDefault(p => p.Id == request.StatusId)?.Name;

                responses.Add(response);
            }

            return responses;
        }

        public async Task<ICollection<Priority>> GetAllPrioritiesAsync() =>
            await taskRepository.GetAllPrioritiesAsync();

        public async Task<ICollection<Status>> GetAllStatusesAsync() =>
            await taskRepository.GetAllStatusesAsync();
    }
}
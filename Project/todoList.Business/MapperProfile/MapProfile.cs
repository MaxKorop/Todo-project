using AutoMapper;
using todoList.DTOS.Requests;
using todoList.DTOS.Responses;
using todoList.Entities.Base;

namespace todoList.Business.MapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            // User

            CreateMap<CreateUserRequest, User>();

            CreateMap<User, UserValidationResponse>();


            // Task

            CreateMap<CreateTaskRequest, _Task>();
            CreateMap<UpdateTaskRequest, _Task>();

            CreateMap<_Task, ViewTaskResponse>();
            CreateMap<_Task, GetTaskResponse>();
        }
    }
}
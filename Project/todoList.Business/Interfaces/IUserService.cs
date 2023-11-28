using System.Collections.Generic;
using System.Threading.Tasks;
using todoList.DTOS.Requests;
using todoList.DTOS.Responses;
using todoList.Entities.Base;

namespace todoList.Business.Interfaces
{
    public interface IUserService
    {
        Task<int> AddUser(CreateUserRequest request);
        Task<int> UpdateUser(User task);
        Task DeleteUser(int id);
        Task<User> GetUserById(int id);
        Task<ICollection<User>> GetUsersAsync();
        Task<bool> IsExist(int id);
        Task<UserValidationResponse> ValidateUserAsync(string userName, string password);
        Task<bool> IsUserNameExistAsync(string userName);
        Task<bool> IsEmailExistAsync(string email);
        Task<int> GetUserIdByUsername(string userName);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using todoList.Business.Interfaces;
using todoList.DataAccess.Repositories.Interfaces;
using todoList.DTOS.Requests;
using todoList.DTOS.Responses;
using todoList.Entities.Base;

namespace todoList.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        public UserService(IUserRepository userRepository,
            IUsersTasksRepository usersTasksRepository, 
            IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<int> AddUser(CreateUserRequest request)
        {
            var user = mapper.Map<User>(request);
            user.Role = "user";

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = hashedPassword;

            var userId = await userRepository.Add(user);
            return userId;
        }

        public async Task<int> UpdateUser(User user) =>
            await userRepository.Update(user);
        public async Task DeleteUser(int id) =>
            await userRepository.DeleteAsync(id);
        public async Task<User> GetUserById(int id) =>
            await userRepository.GetAsync(id);
        public async Task<ICollection<User>> GetUsersAsync() =>
            await userRepository.GetAllAsync();
        public async Task<bool> IsExist(int id) =>
            await userRepository.IsExist(id);
        public async Task<UserValidationResponse> ValidateUserAsync(string userName, string password)
        {
            var user = await userRepository.ValidateUser(userName);
            bool isVerified = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (user == null || !isVerified) return null;

            var response = mapper.Map<UserValidationResponse>(user);
            return response;
        }

        public async Task<bool> IsUserNameExistAsync(string userName) => await userRepository.IsUserNameExist(userName);

        public async Task<bool> IsEmailExistAsync(string email) => await userRepository.IsEmailExist(email);
        public async Task<int> GetUserIdByUsername(string userName) => (await userRepository.ValidateUser(userName)).Id;
    }
}
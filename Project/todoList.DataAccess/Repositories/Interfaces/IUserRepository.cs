using System.Threading.Tasks;
using todoList.Entities.Base;

namespace todoList.DataAccess.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> ValidateUser(string userName);
        public Task<bool> IsUserNameExist(string userName);
        public Task<bool> IsEmailExist(string email);
    }
}
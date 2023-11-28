using System.Security.Claims;
using System.Threading.Tasks;
using todoList.Business.Interfaces;
using todoList.Entities.Base;

namespace todoList.Web.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetId(this ClaimsPrincipal user) => int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
        public static async Task<int> GetIdAsync(this ClaimsPrincipal user) => await Task.FromResult(int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)));

        public static async Task<User> GetUserAsync(this ClaimsPrincipal user, IUserService service) =>
            await service.GetUserById(await user.GetIdAsync());
        public static async Task<User> GetUserAsync(this IUserService service, ClaimsPrincipal user) =>
            await service.GetUserById(await user.GetIdAsync());
    }
}
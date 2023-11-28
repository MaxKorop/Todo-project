using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using todoList.Business.Interfaces;
using todoList.DTOS.Requests;
using todoList.Entities.Base;
using todoList.Web.Models;

namespace todoList.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly ITaskService taskService;
        private readonly IListService listService;

        private readonly ILogger<UserController> logger;
        public UserController(IUserService userService, ILogger<UserController> logger
        ,ITaskService taskService, IListService listService)
        {
            this.userService = userService;
            this.taskService = taskService;
            this.listService = listService;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            
            var userName = User.Identity.Name;
            if (userName == null) return RedirectToAction(nameof(Login));
            
            var userId = await userService.GetUserIdByUsername(userName);
            var lists = await listService.GetAllListOfUserAsync(userId);
            var tasks = await taskService.GetTasksByUserId(userId);
            var user = await userService.GetUserById(userId);

            ViewBag.Lists = lists.ToList();
            ViewBag.Tasks = tasks.ToList();

            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.Name == null) return RedirectToAction(nameof(Index));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            if (!ModelState.IsValid) return View();

            var user = await userService.ValidateUserAsync(model.UserName, model.Password);

            if (user != null)
            {
                ICollection<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                };
                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);

                logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} | '{user.UserName}({user.Id})' logged in.");

                return RedirectToAction(nameof(Index));
            }

            if (await userService.IsUserNameExistAsync(model.UserName))
            {
                logger.LogWarning($"{DateTime.UtcNow.ToLongTimeString()} | '{model.UserName}' tried to login with wrong password.");
                ModelState.AddModelError(nameof(Login), "Your password is wrong.");
            }
            else
            {
                ModelState.AddModelError(nameof(Login), "Username or password is wrong.");
            }
            
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("Email", "UserName", "Password")]CreateUserRequest user)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ViewData.ModelState.Values) {
                    foreach (ModelError error in modelState.Errors) {
                        logger.LogDebug(error.ToString());
                    }
                }

                return View();
            }
            if (await userService.IsEmailExistAsync(user.Email))
            {
                ModelState.AddModelError(nameof(SignUp),"This email already used.");
                return View();
            }

            if (await userService.IsUserNameExistAsync(user.UserName))
            {
                ModelState.AddModelError(nameof(SignUp), "This user name already used.");
                return View();
            }


            int userId = await userService.AddUser(user);
            logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} | New user registered as '{user.UserName}({userId})' with '{user.Email}' email.");
            return RedirectToAction(nameof(Index), "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}

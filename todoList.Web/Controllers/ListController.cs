using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using todoList.Business.Interfaces;
using todoList.Entities.Base;
using todoList.Web.Extensions;

namespace todoList.Web.Controllers
{
    [Authorize]
    public class ListController : Controller
    {
        private readonly IListService listService;
        private readonly ITaskService taskService;
        private readonly IUserService userService;
        private readonly ILogger<ListController> logger;
        public ListController(IListService listService, ITaskService taskService,
            IUserService userService, ILogger<ListController> logger)
        {
            this.listService = listService;
            this.taskService = taskService;
            this.userService = userService;
            this.logger = logger;
        }
        
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? listId)
        {
            if (listId == null) return RedirectToAction(nameof(Index), "User");

            var userId = await User.GetIdAsync();
            ViewBag.Lists = await listService.GetAllListOfUserAsync(userId);

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TList list)
        {
            if(!ModelState.IsValid) return View();

            list.CreatedById = await User.GetIdAsync();;

            int addedListId = await listService.AddList(list);
            var user = await userService.GetUserAsync(User);

            logger.LogInformation($" {DateTime.UtcNow.ToLongTimeString()} | Created new list named '{list.Name} ({addedListId})' by {user.UserName}.");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? listId)
        {
            if (listId == null) return NotFound();
            var id = (int)listId;
            var userId = await User.GetIdAsync();

            ViewBag.Tasks = await taskService.GetTasksByListId(id);
            ViewBag.Lists = await listService.GetAllListOfUserAsync(userId);

            var list = await listService.GetListById((int)listId);

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? listId)
        {   
            if (listId == null)  return NotFound();

            int id = (int)listId;

            if (!await listService.IsExist(id)) return NotFound();

            var list = await listService.GetListById(id);
            var userId = await User.GetIdAsync();

            if (userId != list.CreatedById) return RedirectToAction("AccessDenied", "Home");

            return View(list);

        }

        [HttpPost]
        [ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteTask(int? listId)
        {
            if (listId == null)  return NotFound();

            int id = (int)listId;

            if (!await listService.IsExist(id)) return NotFound();

            var createdById = (await listService.GetListById(id)).CreatedById;
            var userId = await User.GetIdAsync();

            if (userId != createdById) return RedirectToAction("AccessDenied", "Home");

            await listService.DeleteList(id);

            return RedirectToAction(nameof(Index), "User");
        }

    }
}

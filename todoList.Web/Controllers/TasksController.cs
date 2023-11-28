using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using todoList.Business.Interfaces;
using todoList.DTOS.Requests;
using todoList.Entities.Base;
using todoList.Web.Extensions;

namespace todoList.Web.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly IUserService userService;
        private readonly ITaskService taskService;
        private readonly IListService listService;
        private readonly ILogger<TasksController> logger;

        public TasksController(IUserService userService, ITaskService taskService,
            IListService listService, ILogger<TasksController> logger)
        {
            this.userService = userService;
            this.listService = listService;
            this.taskService = taskService;
            this.logger = logger;
        }

        public IActionResult Index(int? listId)
        {
            return RedirectToAction(nameof(Index), "List", new { listId = listId });
        }

        [HttpGet]
        public async Task<IActionResult> Create(int listId)
        {
            ViewBag.Priorities = await GetPriorities();
            ViewBag.Statuses = await GetStatuses();
            ViewBag.User = await GetUser();
            ViewBag.ListId = listId;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTaskRequest task)
        {
            if (ModelState.IsValid)
            {
                User user = await GetUser();
                task.CreatedById = user.Id;

                int addedTaskId = await taskService.AddTask(task);

                logger.LogInformation(
                    $"{DateTime.UtcNow.ToLongTimeString()} | " +
                    $"New task '{task.Name} ({addedTaskId})' added to {task.TListId} list by {user.UserName}"); 

                return RedirectToAction(nameof(Index), "List", new { listId = task.TListId });
            }
            

            foreach (var error in ModelState.Values.SelectMany(stateValue => stateValue.Errors))
            {
                logger.LogWarning(error.ErrorMessage);
            }

            ModelState.AddModelError(nameof(Create), "This model is wrong.");
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(int taskId)
        {
            bool isExist = await taskService.IsExist(taskId);
            if (!isExist) return NotFound();

            var task = await taskService.GetTaskById(taskId);
            var userId = await User.GetIdAsync();

            if (userId != task.CreatedById) return RedirectToAction("AccessDenied", "Home");

            ViewBag.Priorities = await GetPriorities();
            ViewBag.Statuses = await GetStatuses();

            return View(task);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateTaskRequest task)
        {
            if (ModelState.IsValid)
            {
                int affectedRowsCount = await taskService.UpdateTask(task);

                if (affectedRowsCount > 0) return RedirectToAction(nameof(Index), "User");

                return BadRequest();
            }

            logger.LogInformation(
                $"{DateTime.UtcNow.ToLongTimeString()} | " +
                $"New task '{task.Name} ({task.Id})' updated by {User.Identity.Name}");

            ViewBag.Priorities = await GetPriorities();
            ViewBag.Statuses = await GetStatuses();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int taskId)
        {
            if (await taskService.IsExist(taskId) )
            {
                var task = await taskService.GetTaskById(taskId);

                var userId = await User.GetIdAsync();
                if (userId != task.CreatedById) return RedirectToAction("AccessDenied", "Home");

                return View(task);
            }

            return NotFound();
        }

        [HttpPost]
        [ActionName(nameof(Delete))]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            await taskService.DeleteTask(taskId);
            return RedirectToAction(nameof(Index), "User");
        }


        private async Task<List<SelectListItem>> GetPriorities()
        {
            List<SelectListItem> prioritiesList = new List<SelectListItem>();

            var priorities = (await taskService.GetAllPrioritiesAsync()).ToList();

            priorities.ForEach(p => prioritiesList.Add(
                new SelectListItem{ Text=p.Name, Value=p.Id.ToString() } 
            ));

            return prioritiesList;
        }
        private async Task<List<SelectListItem>> GetStatuses()
        {
            List<SelectListItem> statusesList = new List<SelectListItem>();

            var statuses = (await taskService.GetAllStatusesAsync()).ToList();

            statuses.ForEach(s => statusesList.Add(
                new SelectListItem{ Text=s.Name, Value=s.Id.ToString() } 
            ));

            return statusesList;
        }
        private async Task<User> GetUser() => await User.GetUserAsync(userService);
    }
}

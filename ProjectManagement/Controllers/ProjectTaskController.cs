using DB;
using DB.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.ViewModels;
using System.Threading.Tasks;
using System.Transactions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectTaskController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public ProjectTaskController(ApplicationDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public List<TaskListViewModel> GetTasks()
        {
            var list = db.Tasks.Include(e => e.TaskUsers).ThenInclude(e => e.UserTaskLogs).ThenInclude(e => e.Functor).Include(e => e.TaskCategories).ThenInclude(x => x.Category).ToList();
            return db.Tasks.Include(e => e.TaskUsers).ThenInclude(e => e.UserTaskLogs).ThenInclude(e => e.Functor).Include(e => e.TaskCategories).ThenInclude(x => x.Category).Select(t => new TaskListViewModel()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                CreationDate = t.CreationDate,
                TaskDeadline = t.TaskDeadline,
                TaskCategories = t.TaskCategories.Select(x => new TaskCategoryInfoViewModel()
                {
                    CategoryId = x.CategoryId,
                    CategroyTitle = x.Category.Title,
                    Id = x.Id,
                    TaskId = x.TaskId,
                    TaskTitle = x.Task.Title
                }).ToList(),
                TaskUsers = t.TaskUsers.Select(f => new UserInfoViewModel()
                {
                    Id = f.User.Id,
                    FirstName = f.User.FirstName,
                    LastName = f.User.LastName,
                    UserName = f.User.UserName,
                }).ToList(),
                TaskStatus = t.TaskStatus,
                TaskStatusLog = t.TaskUsers.Select(tt => tt.UserTaskLogs.Select(e => new UserTaskLogInfoViewModel()
                {
                    Functor = new UserInfoViewModel()
                    {
                        FirstName = e.Functor.FirstName,
                        LastName = e.Functor.LastName,
                        UserName = e.Functor.UserName,
                        Id = e.FunctorId,
                    },
                    CreationDate = e.CreationDate,
                    FunctorId = e.FunctorId,
                    Id = e.Id,
                    UserTaskId = e.UserTaskId
                }).FirstOrDefault()).ToList()
            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var currentTask = await db.Tasks.Include(e => e.TaskUsers).ThenInclude(e => e.User).Include(e => e.TaskCategories).ThenInclude(x => x.Category).Where(e => e.Id == id).FirstOrDefaultAsync();
            if (currentTask == null)
            {
                return NotFound();
            }
            return Ok(
             new TaskListViewModel()
             {
                 Id = id,
                 Title = currentTask.Title,
                 CreationDate = currentTask.CreationDate,
                 Description = currentTask.Description,
                 TaskDeadline = currentTask.TaskDeadline,
                 TaskCategories = currentTask.TaskCategories.Select(x => new List<TaskCategoryInfoViewModel>()
                    {
                        new TaskCategoryInfoViewModel()
                        {
                            CategoryId = x.CategoryId,
                            CategroyTitle = x.Category.Title,
                            Id = x.Id,
                            TaskId = x.TaskId,
                            TaskTitle = x.Task.Title
                        }
                    }).FirstOrDefault(),
                 TaskUsers = currentTask.TaskUsers.Select(x => new List<UserInfoViewModel>()
                {
                    new UserInfoViewModel()
                    {
                        Id = x.User.Id,
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        UserName = x.User.UserName,
                    }
                }).FirstOrDefault()
             });
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskViewModel vm)
        {
            if (vm is null)
            {
                return BadRequest();
            }
            try
            {
                List<TaskCategory> taskCategoriesList = new List<TaskCategory>();
                await db.Database.BeginTransactionAsync();
                var task = new ProjectTask()
                {
                    Title = vm.Title,
                    Description = vm.Description,
                    CreationDate = DateTime.Now,
                    TaskDeadline = vm.TaskDeadline,
                    TaskStatus = false
                };
                db.Tasks.Add(task);
                await db.SaveChangesAsync();
                if (vm.TaskCategoryIds is not null && vm.TaskCategoryIds.Count > 0)
                {
                    foreach (var item in vm.TaskCategoryIds)
                    {
                        var newTaksCategories = new TaskCategory()
                        {
                            CategoryId = item,
                            Task = task
                        };
                        await db.TaskCategories.AddAsync(newTaksCategories);
                        await db.SaveChangesAsync();
                        taskCategoriesList.Add(newTaksCategories);

                    }
                }
                if (vm.TaskUserIds is not null && vm.TaskUserIds.Count > 0)
                {
                    foreach (var item in vm.TaskUserIds)
                    {
                        var newUserTask = new UserTask()
                        {
                            UserId = item,
                            CreationDate = DateTime.Now,
                            Task = task
                        };
                        await db.UserTasks.AddAsync(newUserTask);
                        await db.SaveChangesAsync();
                    }
                }
                await db.SaveChangesAsync();
                await db.Database.CommitTransactionAsync();
                return Ok(task);
            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditTask([FromBody] TaskViewModel vm)
        {
            var currentTask = await db.Tasks.FindAsync(vm.Id);
            if (currentTask is null)
            {
                return BadRequest();
            }
            try
            {
                await db.Database.BeginTransactionAsync();
                if (vm.TaskCategoryIds != null && vm.TaskCategoryIds.Count > 0)
                {
                    var taskCategories = db.TaskCategories.Where(c => c.TaskId == vm.Id).ToList();
                    db.RemoveRange(taskCategories);
                }
                if (vm.TaskUserIds != null && vm.TaskUserIds.Count > 0)
                {
                    var taskUserIds = db.UserTasks.Where(c => c.TaskId == vm.Id).ToList();
                    db.RemoveRange(taskUserIds);
                }
                await db.SaveChangesAsync();
                if (vm.TaskCategoryIds is not null && vm.TaskCategoryIds.Count > 0)
                {
                    foreach (var item in vm.TaskCategoryIds)
                    {
                        var newTaksCategories = new TaskCategory()
                        {
                            CategoryId = item,
                            Task = currentTask
                        };
                        await db.TaskCategories.AddAsync(newTaksCategories);
                        await db.SaveChangesAsync();
                    }
                }
                if (vm.TaskUserIds is not null && vm.TaskUserIds.Count > 0)
                {
                    foreach (var item in vm.TaskUserIds)
                    {
                        var newUserTask = new UserTask()
                        {
                            UserId = item,
                            CreationDate = DateTime.Now,
                            Task = currentTask,
                        };
                        await db.UserTasks.AddAsync(newUserTask);
                        await db.SaveChangesAsync();
                    }
                }
                currentTask.Title = vm.Title ?? currentTask.Title;
                currentTask.Description = vm.Description ?? currentTask.Description;
                currentTask.TaskDeadline = vm.TaskDeadline ?? currentTask.TaskDeadline;
                currentTask.TaskStatus = vm.TaskStatus??false;
                db.Tasks.Update(currentTask);
                await db.SaveChangesAsync();
                await db.Database.CommitTransactionAsync();
                return Ok();

            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var currentTask = await db.Tasks.FindAsync(id);
            if (currentTask is null)
            {
                return NoContent();
            }
            try
            {
                db.Tasks.Remove(currentTask);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }
        }
    }
}

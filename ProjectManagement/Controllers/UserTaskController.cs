using DB;
using DB.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserTaskController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public UserTaskController(ApplicationDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public List<UserTaskListViewModel> GetAllUserTasks()
        {
            return db.UserTasks.Include(e => e.Task).ThenInclude(e => e.TaskUsers).Include(e => e.Task.TaskCategories).Select(e => new UserTaskListViewModel()
            {
                Id = e.Id,
                TaskStatus = e.TaskStatus,
                CreationDate = e.CreationDate,
                ModifiedDate = e.ModifiedDate,
                Task = new TaskListViewModel()
                {
                    Title = e.Task.Title,
                    CreationDate = e.Task.CreationDate,
                    Id = e.Task.Id,
                    Description = e.Task.Description,
                    TaskDeadline = e.Task.TaskDeadline,
                    TaskCategories = e.Task.TaskCategories.Select(x => new List<TaskCategoryInfoViewModel>()
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
                    TaskUsers = e.Task.TaskUsers.Select(x => new UserInfoViewModel()
                    {
                        Id = e.User.Id,
                        FirstName = e.User.FirstName,
                        LastName = e.User.LastName,
                        UserName = e.User.UserName,
                    }).ToList(),
                },

            }).ToList();
        }

        [HttpGet("{id}")]
        public List<UserTaskListViewModel> GetUserTask(int id)
        {
            return db.UserTasks.Include(e => e.Task).ThenInclude(e => e.TaskUsers).Include(e => e.Task.TaskCategories).Where(e => e.UserId == id).Select(e => new UserTaskListViewModel()
            {
                Id = e.Id,
                TaskStatus = e.TaskStatus,
                CreationDate = e.CreationDate,
                ModifiedDate = e.ModifiedDate,
                Task = new TaskListViewModel()
                {
                    Title = e.Task.Title,
                    CreationDate = e.Task.CreationDate,
                    Id = e.Task.Id,
                    Description = e.Task.Description,
                    TaskDeadline = e.Task.TaskDeadline,
                    TaskCategories = e.Task.TaskCategories.Select(x => new List<TaskCategoryInfoViewModel>()
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
                    TaskUsers = e.Task.TaskUsers.Select(x => new UserInfoViewModel()
                    {
                        Id = e.User.Id,
                        FirstName = e.User.FirstName,
                        LastName = e.User.LastName,
                        UserName = e.User.UserName,
                    }).ToList(),
                },
            }).ToList();
        }

        //[HttpPost]
        //public async Task<IActionResult> CreatUserTask([FromBody] UserTaskViewModel vm)
        //{
        //    if (vm is null)
        //    {
        //        return BadRequest();
        //    }
        //    try
        //    {
        //        var userTask = new UserTask()
        //        {
        //            CreationDate = DateTime.Now,
        //            TaskId = vm.TaskId,
        //            UserId = vm.UserId,
        //            TaskStatus = false,
        //        };
        //        await db.UserTasks.AddAsync(userTask);
        //        await db.SaveChangesAsync();
        //        return Ok(userTask);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content(ex.Message);
        //    }
        //}

        [HttpPut]
        public async Task<IActionResult> EditUserTask([FromBody] UserTaskViewModel vm)
        {
            var currentUserTask = await db.UserTasks.FindAsync(vm.Id);
            if (currentUserTask is null)
            {
                return BadRequest();
            }
            try
            {
                currentUserTask.TaskStatus = vm.TaskStatus ?? false;
                currentUserTask.ModifiedDate = DateTime.Now;
                db.UserTasks.Update(currentUserTask);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUserTask = await db.UserTasks.FindAsync(id);
            if (currentUserTask is null)
            {
                return BadRequest();
            }
            try
            {
                db.UserTasks.Remove(currentUserTask);
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

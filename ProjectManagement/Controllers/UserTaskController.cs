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
        public  List<UserTaskListViewModel> GetAllUserTasks()
        {
            return  db.UserTasks.Select(c => new UserTaskListViewModel()
            {
                Id = c.Id,
                TaskStatus = c.TaskStatus,
                CreationDate = c.CreationDate,
                ModifiedDate = c.ModifiedDate,
                Task = new TaskListViewModel()
                {
                    Title = c.Task.Title,
                    CreationDate = c.Task.CreationDate,
                    Id = c.Task.Id,
                    Description = c.Task.Description,
                    TaskDeadline = c.Task.TaskDeadline,
                    TaskCategories = c.Task.TaskCategories.Select(x => new TaskCategoryInfoViewModel()
                    {
                        CategoryId = x.CategoryId,
                        CategroyTitle = x.Category.Title,
                        Id = x.Id,
                        TaskId = x.TaskId,
                        TaskTitle = x.Task.Title
                    }).ToList(),
                    TaskUsers = c.Task.TaskUsers.Select(f => new UserInfoViewModel()
                    {
                        Id = f.User.Id,
                        FirstName = f.User.FirstName,
                        LastName = f.User.LastName,
                        UserName = f.User.UserName,
                    }).ToList(),
                }
            }).ToList().DistinctBy(e => e.Task.Id).ToList();

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
                    TaskCategories = e.Task.TaskCategories.Select(x => new TaskCategoryInfoViewModel()
                    {
                        CategoryId = x.CategoryId,
                        CategroyTitle = x.Category.Title,
                        Id = x.Id,
                        TaskId = x.TaskId,
                        TaskTitle = x.Task.Title
                    }).ToList(),
                    TaskUsers = e.Task.TaskUsers.Select(x => new UserInfoViewModel()
                    {
                        Id = x.User.Id,
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        UserName = x.User.UserName,
                    }).ToList(),
                },

            }).ToList();
        }

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

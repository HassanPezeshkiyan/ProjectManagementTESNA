using DB;
using DB.Entity;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectManagement.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public UserController(ApplicationDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public List<UserInfoViewModel> GetUsers()
        {
            return db.Users.Select(e => new UserInfoViewModel()
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                UserName = e.UserName,
            }).ToList();


        }

        [HttpGet("{id}")]
        public async Task<UserInfoViewModel> GetUser(int id)
        {
            var user = await db.Users.FindAsync(id);
            if (user is null)
            {
                return null;
            }
            var vm = new UserInfoViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
            };
            return vm;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserViewModel vm)
        {
            if (vm is null)
            {
                return BadRequest();
            }

            var userModel = new User()
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                UserName = vm.UserName,
                Password = vm.Password,
            };
            try
            {
                db.Users.Add(userModel);
                db.SaveChanges();
                return Ok(userModel);
            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> EditUser([FromBody] UserViewModel vm)
        {
            var currentUser = await db.Users.FindAsync(vm.Id);
            if (currentUser is null)
            {
                return NoContent();
            }
            if (vm is not null)
            {
                try
                {
                    currentUser.FirstName = vm.FirstName;
                    currentUser.LastName = vm.LastName;
                    currentUser.UserName = vm.UserName;
                    currentUser.Password = vm.Password;
                    db.Users.Update(currentUser);
                    await db.SaveChangesAsync();
                    return Ok();
                }
                catch (Exception ex)
                {

                    return Content(ex.Message);
                }
            }
            return BadRequest();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await db.Users.FindAsync(id);
            if (user is null)
                return NoContent();

            try
            {
                db.Users.Remove(user);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }

        }
    }
}

using DB;
using DB.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public CategoryController(ApplicationDbContext db)
        {
            this.db = db;
        }
        [HttpGet]
        public List<CategoryViewModel> GetCategories()
        {
            return db.Categories.Include(e => e.Creator).Select(e => new CategoryViewModel
            {
                UserId = e.UserId,
                Title = e.Title,
                Description = e.Description,
                Id = e.Id,
                UserFullName = e.Creator.FirstName + " " + e.Creator.LastName,
            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var currentCategory = await db.Categories.Include(e => e.Creator).Where(e => e.Id == id).FirstOrDefaultAsync();
            if (currentCategory == null)
            {
                return NotFound();
            }
            return Ok(new CategoryViewModel()
            {
                UserId= currentCategory.UserId,
                Title = currentCategory.Title,
                Description = currentCategory.Description,
                Id = currentCategory.Id,
                UserFullName = currentCategory.Creator.FirstName + " " + currentCategory.Creator.LastName
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreatCategory([FromBody] CategoryViewModel vm)
        {
            if (vm is null)
            {
                return BadRequest();
            }
            try
            {

                var model = new Category()
                {
                    Title = vm.Title,
                    Description = vm.Description,
                    UserId = vm.UserId
                };
                db.Categories.Add(model);
                await db.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditCategory([FromBody] CategoryViewModel vm)
        {
            var currentCategory = await db.Categories.FindAsync(vm.Id);
            if (currentCategory == null)
            {
                return NotFound();
            }
            try
            {
                currentCategory.Title = vm.Title??currentCategory.Title;
                currentCategory.Description = vm.Description??currentCategory.Description;
                currentCategory.UserId = vm.UserId;
                db.Categories.Update(currentCategory);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var currentCategory = await db.Categories.FindAsync(id);
            if (currentCategory is null)
            {
                return NoContent();
            }
            try
            {
                db.Categories.Remove(currentCategory);
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

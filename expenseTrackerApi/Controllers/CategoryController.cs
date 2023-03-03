using expenseTrackerApi.Classes;
using expenseTrackerApi.Models;
using expenseTrackerApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace expenseTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private IUnitOfWork unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories() 
        {
            return Ok(await unitOfWork.Categories.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
           var Category = await unitOfWork.Categories.GetAsync(id);
            if (Category is null) return NotFound();
            return Ok(Category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Category entity)
        {
            var res = await unitOfWork.Categories.Create(entity);
            if(res == 1) return StatusCode(StatusCodes.Status201Created);
            return BadRequest();              
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await unitOfWork.Categories.DeleteAsync(id);
            if (res == 1) return Ok("Deleted successfully");
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Category entity)
        {
            var res = await unitOfWork.Categories.Update(entity);
            if (res == 1) return Ok();
            return BadRequest();
        }

    }
}

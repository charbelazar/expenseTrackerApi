using expenseTrackerApi.Models;
using expenseTrackerApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace expenseTrackerApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IUnitOfWork unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;   
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await unitOfWork.Users.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await unitOfWork.Users.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User entity)
        {
            if (entity is null) return BadRequest();
            var res = await unitOfWork.Users.Create(entity);
            if (res == 1)
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await unitOfWork.Users.DeleteAsync(id);
            if (res == 1)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User entity)
        {
            var res = await unitOfWork.Users.Update(entity);
            if(res == 1)
                return Ok();
            else
                return BadRequest();
        }
    }
}

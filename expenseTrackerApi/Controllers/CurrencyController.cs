using expenseTrackerApi.Models;
using expenseTrackerApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace expenseTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : Controller
    {
        private IUnitOfWork unitOfWork;

        public CurrencyController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
        return Ok(await unitOfWork.Currencies.GetAllAsync());  
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) 
        {
            var res = await unitOfWork.Currencies.GetAsync(id);
            if(res == null) StatusCode(StatusCodes.Status201Created); 
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Currency entity)
        {
            var res = await unitOfWork.Currencies.Create(entity);
            if (res == 1) return Ok();
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await unitOfWork.Currencies.DeleteAsync(id);
            if(res ==1) return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Currency entity)
        {
            var res = await unitOfWork.Currencies.Update(entity);
            if (res == 1) return Ok();
            return BadRequest();
        }
    }
}

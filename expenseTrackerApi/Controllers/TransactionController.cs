using expenseTrackerApi.Models;
using expenseTrackerApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace expenseTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        private IUnitOfWork unitOfWork;
        public TransactionController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await unitOfWork.Transactions.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await unitOfWork.Transactions.GetAsync(id));  
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await unitOfWork.Transactions.DeleteAsync(id);
            if (res == 1)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Transaction entity)
        {
            var res = await unitOfWork.Transactions.Create(entity);
            if (res == 1)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Transaction entity)
        {
            var res = await unitOfWork.Transactions.Update(entity);
            if (res == 1)
                return Ok();
            else
                return BadRequest();
        }
    }
}

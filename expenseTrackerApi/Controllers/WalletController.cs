using expenseTrackerApi.Models;
using expenseTrackerApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace expenseTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : Controller
    {
        private IUnitOfWork unitOfWork;
        public WalletController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await unitOfWork.Wallets.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await unitOfWork.Wallets.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Wallet entity)
        {
            if (entity == null) { return BadRequest(); }
            var res = await unitOfWork.Wallets.Create(entity);
            if (res == 1) return StatusCode(StatusCodes.Status201Created);
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await unitOfWork.Wallets.DeleteAsync(id);
            if (res == 1) return Ok();
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Wallet entity)
        {
            var res = await unitOfWork.Wallets.Update(entity);
            if (res == 1) return Ok();
            return BadRequest();
        }
    }
}

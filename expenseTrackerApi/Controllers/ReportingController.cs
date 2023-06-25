using expenseTrackerApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace expenseTrackerApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReportingController : Controller
    {
        private      IUnitOfWork unitOfWork;
        public ReportingController(IUnitOfWork unitofwork)
        {
            this.unitOfWork = unitofwork;
        }

        [HttpGet("[action]/{year}/{wallet_id}")]
        public async Task<IActionResult> GetSalesPerMonth(int year, int wallet_id) 
        {
            return Ok( await unitOfWork.Reporting.GetSalesPerMonthAsync(year, wallet_id));
        }

        [HttpGet("[action]/{year}/{wallet_id}")]
        public async Task<IActionResult> GetMonthWithMostSales(int year, int wallet_id)
        {
            return Ok(await unitOfWork.Reporting.GetMonthWithMostSales(year,wallet_id)); 
        }
    }
}

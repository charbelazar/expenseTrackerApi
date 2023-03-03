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

        [HttpGet("[action]/{year}")]
        public async Task<IActionResult> GetSalesPerMonth(int year) 
        {
            return Ok( await unitOfWork.Reporting.GetSalesPerMonthAsync(year));
        }

        [HttpGet("[action]/{year}")]
        public async Task<IActionResult> GetMonthWithMostSales(int year)
        {
            return Ok(await unitOfWork.Reporting.GetMonthWithMostSales(year)); 
        }
    }
}

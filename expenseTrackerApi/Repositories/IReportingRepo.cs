using expenseTrackerApi.DTOs;

namespace expenseTrackerApi.Repositories
{
    public interface IReportingRepo
    {
        public Task<IEnumerable<SalesPerMonth>> GetSalesPerMonthAsync(int year);
        public Task<SalesPerMonth> GetMonthWithMostSales(int year);
    }
}

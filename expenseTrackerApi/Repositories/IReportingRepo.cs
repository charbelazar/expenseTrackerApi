using expenseTrackerApi.DTOs;

namespace expenseTrackerApi.Repositories
{
    public interface IReportingRepo
    {
        public Task<IEnumerable<SalesPerMonth>> GetSalesPerMonthAsync(int year , int wallet_id);
        public Task<SalesPerMonth> GetMonthWithMostSales(int year, int wallet_id);
    }
}

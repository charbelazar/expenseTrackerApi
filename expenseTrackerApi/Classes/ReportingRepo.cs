using Dapper;
using expenseTrackerApi.DTOs;
using expenseTrackerApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace expenseTrackerApi.Classes
{
    public class ReportingRepo : IReportingRepo
    {

        private IConfiguration configuration;
        private string connectionstring;

        public ReportingRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionstring = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<SalesPerMonth> GetMonthWithMostSales(int year)
        {
            using(SqlConnection cnsql = new SqlConnection(connectionstring))
            {
                cnsql.Open();
                string query = "SELECT DATENAME(MONTH, TransactionDate) as  MonthName \r\n\t," +
                                                                                  "ISNULL(SUM(Transactions.Amount), 0) AS TotalSales" +
                                                                        "\r\nFROM [Transactions]" +
                                                                           " where year(TransactionDate) = @FilterYear and IsExpense = 1  " +
                                                                           "\r\nGROUP BY DATENAME(MONTH, TransactionDate)\r\n" +
                                                                           "HAVING ISNULL(SUM(Transactions.Amount), 0) = (\r\n\t\tSELECT MAX(AMOUNT)\r\n\t\tFROM (\r\n\t\t\tSELECT MAX(AMOUNT) AS AMOUNT\r\n\t\t\tFROM Transactions\r\n\t\t\tGROUP BY DATENAME(MONTH, TransactionDate)\r\n\t\t\t) T\r\n\t\t)";
                return await cnsql.QueryFirstOrDefaultAsync<SalesPerMonth>(query, new { FilterYear = year});
            }
        }


        public async Task<IEnumerable<SalesPerMonth>> GetSalesPerMonthAsync(int year)
        {
            using(SqlConnection cnsql = new SqlConnection(connectionstring))
            {
                cnsql.Open();
                string query = "WITH" +
                    "  Months(MonthNum, MonthName) AS (\r\n  " +
                      "SELECT 1, 'January' UNION ALL\r\n" +
                    "  SELECT 2, 'February' UNION ALL\r\n  " +
                      "SELECT 3, 'March' UNION ALL\r\n" +
                    "  SELECT 4, 'April' UNION ALL\r\n" +
                    "  SELECT 5, 'May' UNION ALL\r\n" +
                    "  SELECT 6, 'June' UNION ALL\r\n  " +
                      "SELECT 7, 'July' UNION ALL\r\n  " +
                      "SELECT 8, 'August' UNION ALL\r\n  " +
                      "SELECT 9, 'September' UNION ALL\r\n  " +
                      "SELECT 10, 'October' UNION ALL\r\n  " +
                      "SELECT 11, 'November' UNION ALL\r\n" +
                    "  SELECT 12, 'December'\r\n)\r\n" +
                      "SELECT\r\n  Months.MonthName as MonthName,\r\n  ISNULL(SUM(Transactions.Amount), 0) AS TotalSales\r\n" +
                      "FROM Months\r\nLEFT JOIN [ExpenseTracker].[dbo].[Transactions]\r\nON Months.MonthNum = MONTH(Transactions.TransactionDate) AND \r\n YEAR(Transactions.TransactionDate) = @FilterYear and IsExpense = 1 \r\nGROUP BY Months.MonthNum, Months.MonthName\r\nORDER BY Months.MonthNum";

                return await cnsql.QueryAsync<SalesPerMonth>(query, new { FilterYear = year });
            }
        }
    }
}

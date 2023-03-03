using Dapper;
using expenseTrackerApi.Models;
using expenseTrackerApi.Repositories;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.Data.SqlClient;
using static Dapper.SqlMapper;

namespace expenseTrackerApi.Classes
{
    public class CurrencyRepo : ICurrencyRepo
    {
        private IConfiguration configuration;
        private string connectionstring;

        public CurrencyRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionstring = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<int> Create(Currency entity)
        {
            using(SqlConnection cnsql = new SqlConnection(connectionstring)) 
            {
                cnsql.Open();
                return await cnsql.ExecuteAsync("Insert into Currency values (@CurrencyCode , @CurrencySymbol) ", entity);
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (SqlConnection cnsql = new SqlConnection(connectionstring))
            {
                cnsql.Open();
                return  await cnsql.ExecuteAsync("delete from Currency where id = @id ", new {id= id});
            }
        }

        public async Task<IEnumerable<Currency>> GetAllAsync()
        {
            using (SqlConnection cnsql = new SqlConnection(connectionstring))
            {
                cnsql.Open();
                return await cnsql.QueryAsync<Currency>("Select * from Currency");
            }
        }

        public  async Task<Currency> GetAsync(int id)
        {
            using (SqlConnection cnsql = new SqlConnection(connectionstring))
            {
                cnsql.Open();
                return await cnsql.QueryFirstOrDefaultAsync<Currency>("Select * from Currency where id = @id", new
                {
                    id = id
                });
            }
        }

        public  async Task<int> Update(Currency entity)
        {
            using (SqlConnection cnsql = new SqlConnection(connectionstring))
            {
                cnsql.Open();
                return await cnsql.ExecuteAsync("Update Currency set  CurrencySymbol = @CurrencySymbol ," +
                                                                 "CurrenyCode = @CurrencyCode " +
                                                                 "where id = @id", entity);
            }
        }
    }
}

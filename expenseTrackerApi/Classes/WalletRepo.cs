using Dapper;
using expenseTrackerApi.Models;
using expenseTrackerApi.Repositories;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using static Dapper.SqlMapper;

namespace expenseTrackerApi.Classes
{
    public class WalletRepo : IWalletRepo
    {
        private IConfiguration configuration;
        private string connectionstring;

        public WalletRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionstring = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<int> Create(Wallet entity)
        {
            using(SqlConnection cnsql = new SqlConnection(connectionstring)) 
            {
                cnsql.Open();
                return await cnsql.ExecuteAsync("Insert into wallet (InitialSum , currency_id , Name , User_id)" +
                    " values (@InitialSum , @currency_id , @Name, @User_id)", entity);
            } 
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (SqlConnection cnsql = new SqlConnection(connectionstring))
            {
                cnsql.Open();
                return await cnsql.ExecuteAsync("delete from Wallet where id = @id", new {id = id});
            }
        }

        public async Task<IEnumerable<Wallet>> GetAllAsync()
        {
            using (SqlConnection cnsql = new SqlConnection(connectionstring))
            {
                cnsql.Open();
                return await cnsql.QueryAsync<Wallet>("select * from wallet");
            }
        }

        public async Task<Wallet> GetAsync(int id)
        {
            using (SqlConnection cnsql = new SqlConnection(connectionstring))
            { 
                cnsql.Open();
                return await cnsql.QueryFirstOrDefaultAsync<Wallet>("select * from wallet where id = @id",new {id = id });
            }
        }

        public async Task<int> Update(Wallet entity)
        {
            using (SqlConnection cnsql = new SqlConnection(connectionstring))
            {
                cnsql.Open();
                return await cnsql.ExecuteAsync("update wallet set  InitialSum = @InitialSum ," +
                                                             "Name = @Name , " +
                                                             "currency_id = @currency_id , " +
                                                             "User_id = @User_id " +
                                                             "  where id = @id", entity);
            }
        }
    }
}

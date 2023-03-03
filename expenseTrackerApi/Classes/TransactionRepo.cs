using Dapper;
using expenseTrackerApi.Models;
using expenseTrackerApi.Repositories;
using System.Data.SqlClient;
using static Dapper.SqlMapper;

namespace expenseTrackerApi.Classes
{
    public class TransactionRepo : ITransactionRepo
    {

        private IConfiguration configuration;
        private string connectionstring;
        public TransactionRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionstring = this.configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> Create(Transaction entity)
        {
            using (SqlConnection cnsql = new SqlConnection(connectionstring)) 
            {
                cnsql.Open();                
                entity.TransactionDate = DateTime.Now;
                return await cnsql.ExecuteAsync("Insert into Transactions (Amount, IsExpense , TransactionDate , Category_id , Wallet_id)" +
                    " values (@Amount , @IsExpense ,@TransactionDate , @Category_id , @Wallet_id )", entity);
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (SqlConnection cnsql = new SqlConnection(connectionstring))
            {
                cnsql.Open();            
                return await cnsql.ExecuteAsync("Delete from transactions where id = @id )", new {id = id  });
            }
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            using(SqlConnection cnsql = new SqlConnection(connectionstring))
            {
                cnsql.Open();
                return  await cnsql.QueryAsync<Transaction>("Select * from transactions"); 
            }
        }

        public async Task<Transaction> GetAsync(int id)
        {
            using(SqlConnection cnsql = new SqlConnection(connectionstring))
            {
                cnsql.Open();
                return await cnsql.QueryFirstOrDefaultAsync<Transaction>("Select * from transactions where id = @id", new { id = id });
            }
        }

        public async Task<int> Update(Transaction entity)
        {
            using (SqlConnection cnsql = new SqlConnection(connectionstring))
            {
                cnsql.Open();
                return await cnsql.ExecuteAsync("Update Transcations set  Amount = @Amount," +
                                                                   "Category_id = @Category_id," +
                                                                   "IsExpense = @IsExpense," +
                                                                   "TransactionDate = @TransactionDate" +
                                                               "where id = @id");
            }
        }
    }
}

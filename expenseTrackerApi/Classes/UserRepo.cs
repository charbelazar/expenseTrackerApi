using Dapper;
using expenseTrackerApi.Models;
using expenseTrackerApi.Repositories;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.Data.SqlClient;
using System.Diagnostics;
using static Dapper.SqlMapper;

namespace expenseTrackerApi.Classes
{
    public class UserRepo : IUserRepo
    {

        private string connectionstring;
        private IConfiguration config;

        public UserRepo( IConfiguration config)
        {
          
            this.config = config;
            this.connectionstring = this.config.GetConnectionString("Defaultconnection");
        }
    
        public async Task<int> Create(User entity)
        {
         using(SqlConnection cnsql = new SqlConnection(connectionstring)) 
            {
                cnsql.Open();
                var res =  await cnsql.ExecuteAsync("Insert into Users values (@Name , @Password)", entity);
                return res;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (SqlConnection cnsql = new SqlConnection(connectionstring))
            {
                cnsql.Open();
                var res = await cnsql.ExecuteAsync("delete from Users where id =@id", new {id = id});
                return res;
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using (SqlConnection cnsql = new SqlConnection(connectionstring))
            {
                cnsql.Open();
                return await cnsql.QueryAsync<User>("Select * from Users"); 
            }
        }

        public async Task<User> GetAsync(int id)
        {
            using (SqlConnection cnsql = new SqlConnection(connectionstring))
            {
                cnsql.Open();
                return await cnsql.QueryFirstOrDefaultAsync<User>("Select * from Users where id = @id",new {id = id});
            }
        }

        public async Task<int> Update(User entity)
        {
            using (SqlConnection cnsql = new SqlConnection(connectionstring))
            {
                cnsql.Open();
                var res = await cnsql.ExecuteAsync("Update Users set  Name = @Name," +
                                                                    "password = @Password " +
                                                                    "where id = @id ", entity);
                return res;
            }
        }
    }
}

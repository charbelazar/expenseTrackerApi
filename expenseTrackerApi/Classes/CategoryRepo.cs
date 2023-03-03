using expenseTrackerApi.Models;
using expenseTrackerApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Data.SqlClient;
using Dapper;
using static Dapper.SqlMapper;

namespace expenseTrackerApi.Classes
{
    public class CategoryRepo : ICategoryRepo
    {
        private IConfiguration configuration;
        private string connectionstring;
        public CategoryRepo(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionstring = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<int> Create(Category entity)
        {
          using(SqlConnection cnsql = new SqlConnection(connectionstring)) 
            {
                cnsql.Open();
                return await cnsql.ExecuteAsync("insert into Category values (@Name)",entity);
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (SqlConnection cnsql = new SqlConnection(connectionstring))
            {
                cnsql.Open();
                return await cnsql.ExecuteAsync("Delete from Category where id = @id", new { id = id});
            }
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            using (SqlConnection cnsql = new SqlConnection(connectionstring))
            {
                cnsql.Open();
                return await cnsql.QueryAsync<Category>("Select * from Category");
            }
        }


        public async Task<Category> GetAsync(int id)
        {
            using (SqlConnection cnsql = new SqlConnection(connectionstring))
            {
                cnsql.Open();
                return await cnsql.QueryFirstOrDefaultAsync<Category>("Select * from Category where id = @id", new { id = id});
            };
        }

        public async Task<int> Update(Category entity)
        {
            using (SqlConnection cnsql = new SqlConnection(connectionstring))
            {
                cnsql.Open();
                return await cnsql.ExecuteAsync("Update Category set name = @name  where id=@id",entity);
            }
        }
    }
}

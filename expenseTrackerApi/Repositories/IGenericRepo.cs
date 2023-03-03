using Microsoft.AspNetCore.Mvc;

namespace expenseTrackerApi.Repositories
{ 
    public interface IGenericRepo<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetAsync(int id);
        public Task<int> DeleteAsync(int id);
        public Task<int> Create(T entity);
        public Task<int> Update(T entity);

    }
}

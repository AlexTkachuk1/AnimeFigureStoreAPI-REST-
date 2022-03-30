using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task<bool> CreateOrUpdateAsync(T data);
        Task<bool> DeleteAsync(Guid id);
    }
}

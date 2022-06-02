using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InteliSystem.Utils.Interfaces
{
    public interface IRepositoryCrud : IRepository
    {
        Task<int> AddAsync<T>(T tobjct) where T : class;
        Task<int> DeleteAsync<T>(T tobjct) where T : class;
        Task<IEnumerable<T>> GetAllAsync<T>(dynamic? filter = null) where T : class;
        Task<T> GetAsync<T>(T tobjct) where T : class;
        Task<int> UpdateAsync<T>(T tobjct) where T : class;
    }
}
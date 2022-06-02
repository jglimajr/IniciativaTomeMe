using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InteliSystem.Utils.Interfaces
{
    public interface IRepositoryBaseCrud<T> : IRepository where T : class
    {
        Task<int> AddAsync(T tobjct);
        Task<int> DeleteAsync(T tobjct);
        Task<IEnumerable<T>> GetAllAsync(object? filter = null);
        Task<T> GetAsync(T tobjct);
        Task<int> UpdateAsync(T tobjct);
    }
}
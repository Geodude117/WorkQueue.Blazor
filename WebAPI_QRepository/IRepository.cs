using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_QRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(int accountNumber);
        Task<int?> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int WescotRef);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainData.Repository.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(int accountNumber);
        Task<int?> Insert(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int WescotRef);
        Task<IEnumerable<T>> GetAllAsync();

    }
}

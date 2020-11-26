using DomainData.Models;
using DomainData.Repository.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainData.Repository.DomainTypeRepo
{
    public interface IDomainTypeRepo : IRepository<DomainType>
    {
        new Task<bool> DeleteAsync(int Id);
        new Task<DomainType> GetAsync(int Id);
        new Task<int?> Insert(DomainType entity);
        new Task<bool> UpdateAsync(DomainType entity);
        new Task<IEnumerable<DomainType>> GetAllAsync();

    }
}
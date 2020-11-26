using DomainData.Models;
using DomainData.Repository.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainData.Repository.DomainInformationRepo
{
    public interface IDomainInformationRepo : IRepository<DomainInformation>
    {
        new Task<bool> DeleteAsync(int Id);
        new Task<DomainInformation> GetAsync(int Id);
        new Task<int?> Insert(DomainInformation entity);
        new Task<bool> UpdateAsync(DomainInformation entity);

        new Task<IEnumerable<DomainInformation>> GetAllAsync();

        Task<IEnumerable<DomainInformation>> GetAllByGroupIdAsync(int GroupId);
    }
}
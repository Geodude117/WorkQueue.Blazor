using DomainData.Models;
using DomainData.Repository.Repository;
using System.Threading.Tasks;

namespace DomainData.Repository.DomainGroupRepo
{
    public interface IDomainGroupRepo : IRepository<DomainGroup>
    {
        new Task<bool> DeleteAsync(int Id);
        new Task<DomainGroup> GetAsync(int Id);
        new Task<int?> Insert(DomainGroup entity);
        new Task<bool> UpdateAsync(DomainGroup entity);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainData.BusinessLogic.DomainGroup
{
    public interface IDomainGroupBusiness
    {
        Task<IEnumerable<Models.DomainGroup>> GetAllDomainGroup();

        Task<Models.DomainGroup> GetDomainGroup(int id);

        Task<int?> PostDomainGroup(Models.DomainGroup domainGroup);

    }
}
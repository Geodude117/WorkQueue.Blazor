using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainData.BusinessLogic.DomainType
{
    public interface IDomainTypeBusiness
    {
        Task<IEnumerable<Models.DomainType>> GetAllDomainType();
        Task<Models.DomainType> GetDomainType(int id);

        Task<int?> PostDomainType(Models.DomainType domainType);

    }
}
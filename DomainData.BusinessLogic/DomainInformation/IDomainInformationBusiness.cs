using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainData.BusinessLogic.DomainInformation
{
    public interface IDomainInformationBusiness
    {
        Task<IEnumerable<Models.DomainInformation>> GetDomainInformationByGroupId(int groupId);

        Task<int?> PostDomainInformation(Models.DomainInformation domainInfo);

    }
}
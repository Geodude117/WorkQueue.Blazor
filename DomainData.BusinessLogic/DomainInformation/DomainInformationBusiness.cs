using DomainData.Repository.DomainInformationRepo;
using DomainData.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainData.BusinessLogic.DomainInformation
{
    public class DomainInformationBusiness : IDomainInformationBusiness
    {
        private readonly IDomainInformationRepo _informationRepo;
        public DomainInformationBusiness(IUnitOfWork unitOfWork)
        {
            _informationRepo = unitOfWork.InformationRepo;
        }

        async Task<IEnumerable<Models.DomainInformation>> IDomainInformationBusiness.GetDomainInformationByGroupId(int groupId)
        {
            var result = await _informationRepo.GetAllByGroupIdAsync(groupId);
            if (result == null)
                throw new NullReferenceException("Database contains no DomainInformation.");
            return result;
        }

        async Task<int?> IDomainInformationBusiness.PostDomainInformation(Models.DomainInformation domainInfo)
        {
            var result = await _informationRepo.Insert(domainInfo);
            if (result == null)
                throw new NullReferenceException("Database contains no DomainInformation.");
            return result;
        }
    }
}

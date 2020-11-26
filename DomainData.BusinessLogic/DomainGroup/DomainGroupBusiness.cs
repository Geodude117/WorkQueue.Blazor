using DomainData.Repository.DomainGroupRepo;
using DomainData.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DomainData.BusinessLogic.DomainGroup
{
    public class DomainGroupBusiness : IDomainGroupBusiness
    {
        private readonly IDomainGroupRepo _groupRepo;
        public DomainGroupBusiness(IUnitOfWork unitOfWork)
        {
            _groupRepo = unitOfWork.GroupRepo;
        }

        public async Task<IEnumerable<DomainData.Models.DomainGroup>> GetAllDomainGroup()
        {
            var result = await _groupRepo.GetAllAsync();
            if (result == null)
                throw new NullReferenceException("Database contains no DomainGroup.");
            return result;
        }

        public async Task<DomainData.Models.DomainGroup> GetDomainGroup(int id)
        {
            var result = await _groupRepo.GetAsync(id);
            if (result == null)
                throw new NullReferenceException("Database contains no DomainGroup.");
            return result;
        }


        public async Task<int?> PostDomainGroup(DomainData.Models.DomainGroup domainGroup)
        {
            var result = await _groupRepo.Insert(domainGroup);
            if (result == null)
                throw new NullReferenceException("Database contains no DomainGroup.");
            return result;
        }
    }
}

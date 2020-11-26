using DomainData.Repository.DomainTypeRepo;
using DomainData.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DomainData.BusinessLogic.DomainType
{
    public class DomainTypeBusiness : IDomainTypeBusiness
    {
        private readonly IDomainTypeRepo _typeRepo;
        public DomainTypeBusiness(IUnitOfWork unitOfWork)
        {
            _typeRepo = unitOfWork.TypeRepo;
        }

        public async Task<IEnumerable<DomainData.Models.DomainType>> GetAllDomainType()
        {
            var result = await _typeRepo.GetAllAsync();
            if (result == null)
                throw new NullReferenceException("Database contains no DomainType.");
            return result;
        }

        public async Task<DomainData.Models.DomainType> GetDomainType(int id)
        {
            var result = await _typeRepo.GetAsync(id);
            if (result == null)
                throw new NullReferenceException("Database contains no DomainType.");
            return result;
        }

        public async Task<int?> PostDomainType(DomainData.Models.DomainType domainType)
        {
            var result = await _typeRepo.Insert(domainType);
            if (result == null)
                throw new NullReferenceException("Database contains no DomainType.");
            return result;
        }
    }
}

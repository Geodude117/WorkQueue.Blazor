using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CallBack_Model.Model;
using WebAPI_QRepository;
using WebAPI_QRepository.Specific_Repo.QGroup;

namespace WebAPI_QBusiness.QueueGroup_Logic
{
    public class QueueGroupBusiness : IQueueGroupBusiness
    {
        private IQGroupRepo _groupRepo;
        public QueueGroupBusiness(IUnitOfWork unitOfWork)
        {
            _groupRepo = unitOfWork.QGroupRepo;
        }

        public async Task<IEnumerable<QueueGroup>> GetQGroup()
        {
            var result = await _groupRepo.GetAllAsync();
            if (result == null)
                throw new NullReferenceException("Database contains no QGroups.");
            return result;
        }
    }
}

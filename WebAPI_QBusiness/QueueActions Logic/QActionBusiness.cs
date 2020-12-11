using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CallBack_Model.Model;
using WebAPI_QRepository;
using WebAPI_QRepository.Specific_Repo.QActionsRepo;

namespace WebAPI_QBusiness.QueueActions_Logic
{
    public class QActionBusiness : IQActionBusiness
    {
        private readonly IQActionRepo _qActionRepo;
        public QActionBusiness(IUnitOfWork unitOfWork)
        {
            _qActionRepo = unitOfWork.QActionRepo;
        }

        public async Task<IEnumerable<QueueAction>> GetAllActions(int QueueResultID)
        {
           return await _qActionRepo.GetActions(QueueResultID);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CallBack_Model.Model;
using WebAPI_QRepository;
using WebAPI_QRepository.Specific_Repo.QRepo;

namespace WebAPI_QBusiness.Indivdual_Queues
{
    /// <summary>
    /// Deals with retriving and  adding to the core queue, as in creating more queues.
    /// </summary>
    public class QBusiness : IQBusiness
    {
        private readonly IQRepo _qRepo;
        public QBusiness(IUnitOfWork unitofwork)
        {
            _qRepo = unitofwork.QRepo;
        }

        public async Task<QueueModel> GetQAsync(int QId)
        {
            return await _qRepo.GetAsync(QId);
        }

        public async Task<IEnumerable<QueueModel>> GetQCollectionAsync()
        {
            return await _qRepo.GetAllAsync();
        }
    }
}

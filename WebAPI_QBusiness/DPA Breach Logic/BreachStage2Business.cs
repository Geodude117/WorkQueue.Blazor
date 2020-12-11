using CallBack_Model.Model;
using DomainData.Models.DPABreachModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebAPI_QBusiness.QueueActions_Logic;
using WebAPI_QRepository;
using WebAPI_QRepository.Specific_Repo.DPABreachStage2;

namespace WebAPI_QBusiness.DPA_Breach_Logic
{
    public class BreachStage2Business : IBreachStage2Business
    {
        private readonly IBreachStage2Repo _dpaBreachStage2Repo;
        private readonly IQueueBusiness _queueBusiness;
        private readonly IQActionBusiness _qActionsBusiness;

        public BreachStage2Business(IUnitOfWork unitOfWork, IQueueBusiness qBusiness, IQActionBusiness qActions)
        {
            _queueBusiness = qBusiness;
            _dpaBreachStage2Repo = unitOfWork.BreachStage2Repo;
            _qActionsBusiness = qActions;
        }

        public Task<bool> DeleteBreachStage2(int QueueItemID)
        {
            throw new NotImplementedException();
        }

        public async Task<BreachStage2> GetBreachStage2Async(int QueueItemID)
        {
            return await _dpaBreachStage2Repo.GetAsync(QueueItemID);
        }

        public async Task<bool> PostAsync(QueueItem QItem, BreachStage2 BreachStage2)
        {
            return await CreateDPABreachStage2(QItem, BreachStage2);
        }

        public async Task<bool> PutBreachStage2EditAsync(BreachStage2 BreachStage2)
        {
          
            return await _dpaBreachStage2Repo.UpdateAsync(BreachStage2);
        }

     
        public async Task<bool> PutBreachStage2EditCompleteAsync(int QueueResultID, QueueItem QItem, BreachStage2 BreachStage2)
        {
            var results = await _qActionsBusiness.GetAllActions(QueueResultID);

            var result2 = await _dpaBreachStage2Repo.UpdateAsync(BreachStage2);

            var result3 = await _queueBusiness.Put_QueueItemComplete(QItem);
            

            return result3;

        }

        private async Task<bool> CreateDPABreachStage2(QueueItem QItem, BreachStage2 dpaBreachStage2Item)
        {
            var queueItemresponse = await _queueBusiness.Post_QueueItem(QItem);

            if (queueItemresponse == null)
                return false;

            dpaBreachStage2Item.QueueItemID = queueItemresponse;

            return await _dpaBreachStage2Repo.InsertAsync(dpaBreachStage2Item) != null;
        }


    }

}

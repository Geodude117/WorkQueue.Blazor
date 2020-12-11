using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CallBack_Model.Model;
using DPABreachModel;
using WebAPI_QBusiness.QueueActions_Logic;
using WebAPI_QRepository;
using WebAPI_QRepository.Specific_Repo.DPABreachStage1;

namespace WebAPI_QBusiness.DPA_Breach_Logic
{
    public class BreachStage1Business : IBreachStage1Business
    {
        private readonly IBreachStage1Repo _dpaBreachStage1Repo;
        private readonly IQueueBusiness _queueBusiness;
        private readonly IQActionBusiness _qActionsBusiness;

        public BreachStage1Business(IUnitOfWork unitOfWork, IQueueBusiness qBusiness, IQActionBusiness qActions)
        {
            _queueBusiness = qBusiness;
            _dpaBreachStage1Repo = unitOfWork.BreachStage1Repo;
            _qActionsBusiness = qActions;
        }

        public Task<bool> DeleteBreachStage1(int QueueItemID)
        {
            throw new NotImplementedException();
        }

        public async Task<BreachStage1> GetBreachStage1Async(int QueueItemID)
        {
            return await _dpaBreachStage1Repo.GetAsync(QueueItemID);
        }

        public async Task<bool> PostAsync(QueueItem QItem, BreachStage1 BreachStage1)
        {
            return await CreateDPABreachStage1(QItem, BreachStage1);
        }

        public async Task<bool> PutBreachStage1EditAsync(BreachStage1 BreachStage1)
        {
            if (BreachStage1.CompareTo(await _dpaBreachStage1Repo.GetAsync(BreachStage1.QueueItemID.Value)) < 0)
                return false;
            return await _dpaBreachStage1Repo.UpdateAsync(BreachStage1);
        }

        public async Task<bool> PutBreachStage1EditCompleteAsync(int QueueResultID, QueueItem QItem, BreachStage1 BreachStage1)
        {
        
            var results = await _qActionsBusiness.GetAllActions(QueueResultID);
            
            var result2 = await _dpaBreachStage1Repo.UpdateAsync(BreachStage1);

            var result3 = await _queueBusiness.Put_QueueItemComplete(QItem);
            
            QItem.CreatedDate = DateTime.Now;
            var temp = QItem.CreatedDate;

            var tempResult = temp.AddHours(2);
            var dueDate = tempResult.Date;

            QItem.DueDate = dueDate;
            QItem.ParentQueueItemID = QItem.QueueItemID;
            QItem.CreatedBy = QItem.CompletedBy;
            QItem.QueueItemID = null;
            QItem.CompletedBy = null;
            QItem.CompletedDate = null;

            var queueItemresponse = await _queueBusiness.Post_QueueItem(QItem);

            return result3;
            
        }
        
        private async Task<bool> CreateDPABreachStage1(QueueItem QItem, BreachStage1 dpaBreachStage1Item)
        {
            var queueItemresponse = await _queueBusiness.Post_QueueItem(QItem);

            if (queueItemresponse == null)
                return false;

            dpaBreachStage1Item.QueueItemID = queueItemresponse;

            return await _dpaBreachStage1Repo.InsertAsync(dpaBreachStage1Item) != null;
        }


    }
}

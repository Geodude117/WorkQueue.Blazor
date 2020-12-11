using CallBack_Model.Model;
using DomainData.Models.DPABreachModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_QBusiness.DPA_Breach_Logic
{
    public interface IBreachStage1Business 
    {
        Task<BreachStage1> GetBreachStage1Async(int QueueItemID);

        Task<bool> PostAsync(QueueItem QItem, BreachStage1 BreachStage1);

        Task<bool> PutBreachStage1EditAsync(BreachStage1 BreachStage1);

        Task<bool> PutBreachStage1EditCompleteAsync(int Id, QueueItem QueueItem,  BreachStage1 BreachStage1);

        Task<bool> DeleteBreachStage1(int QueueItemID);
    }
}

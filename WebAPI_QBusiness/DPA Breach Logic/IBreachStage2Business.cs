using CallBack_Model.Model;
using DPABreachModel;
using System.Threading.Tasks;

namespace WebAPI_QBusiness.DPA_Breach_Logic
{
    public interface IBreachStage2Business
    {
        Task<BreachStage2> GetBreachStage2Async(int QueueItemID);

        Task<bool> PostAsync(QueueItem QItem, BreachStage2 BreachStage2);

        Task<bool> PutBreachStage2EditAsync(BreachStage2 BreachStage2);

        Task<bool> PutBreachStage2EditCompleteAsync(int Id, QueueItem QueueItem, BreachStage2 BreachStage2);

        Task<bool> DeleteBreachStage2(int QueueItemID);

    }
}

using CallBack_Model.Interface;
using CallBack_Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_QBusiness
{
    public interface ICSUBusiness : ICallBackItem<CSU_Callback>
    {
        Task<CSU_Callback> GetCSUItem(int QueueItemID);

        Task<bool> Post(QueueItem QItem, CSU_Callback CSUitem);

        Task<bool> PutCSUItemEdit(CSU_Callback CSUitem);

        Task<bool> DeleteCSUItem(int QueueItemID);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CallBack_Model.Interface;
using CallBack_Model.Model;

namespace WebAPI_QBusiness
{
    public interface IQueueBusiness
    {
        Task<IEnumerable<QueueItem>> Get_QueueItmsAsync(int QueueGroupID);

        Task<QueueItem> Get_QueueItm(int QueueItemID);

        Task<IEnumerable<QueueItem>> Get_QueueItms(SearchParameters search);

        Task<int?> Post_QueueItem(QueueItem itmQueueItem);

        Task<bool> Put_QueueItemEditAsync(QueueItem imtQueueItem);

        Task<bool> Put_QueueItemComplete(QueueItem itmQueueItem);

        Task<bool> Delete_QueueItem(int id);
    }
}

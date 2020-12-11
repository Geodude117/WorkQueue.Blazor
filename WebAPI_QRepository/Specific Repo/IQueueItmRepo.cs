using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CallBack_Model.Interface;
using CallBack_Model.Model;

namespace WebAPI_QRepository.Specific_Repo
{
    public interface IQueueItmRepo: IRepository<QueueItem>
    {
        Task<IEnumerable<QueueItem>> GetByGroupAsync(int QueueGroupID);

        Task<IEnumerable<QueueItem>> GetSearchAsync(SearchParameters search);
    }
}

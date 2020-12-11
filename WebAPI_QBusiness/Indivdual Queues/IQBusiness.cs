using CallBack_Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_QBusiness.Indivdual_Queues
{
    public interface IQBusiness
    {
        Task<IEnumerable<QueueModel>> GetQCollectionAsync();

        Task<QueueModel> GetQAsync(int QId);

    }
}

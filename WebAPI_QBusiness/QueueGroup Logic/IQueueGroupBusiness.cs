using CallBack_Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_QBusiness.QueueGroup_Logic
{
    public interface IQueueGroupBusiness
    {
        Task<IEnumerable<QueueGroup>> GetQGroup();
    }
}

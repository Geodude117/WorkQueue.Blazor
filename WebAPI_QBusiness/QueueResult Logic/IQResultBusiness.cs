using CallBack_Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_QBusiness.QueueResult
{
    public interface IQResultBusiness
    {
        Task<IEnumerable<QResult>> GetQResult(int QueueID);
    }
}

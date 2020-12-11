using CallBack_Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_QBusiness.QueueActions_Logic
{
    public interface IQActionBusiness
    {
        Task<IEnumerable<QueueAction>> GetAllActions(int QueueResultID);
    }
}

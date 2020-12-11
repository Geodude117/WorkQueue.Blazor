using CallBack_Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_QRepository.Specific_Repo.QActionsRepo
{
    public interface IQActionRepo: IRepository<QueueAction>
    {
        Task<IEnumerable<QueueAction>> GetActions(int QueueResultID);
    }
}

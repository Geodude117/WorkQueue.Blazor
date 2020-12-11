using CallBack_Model.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_QRepository.Specific_Repo.QGroup
{
    public interface IQGroupRepo : IRepository<QueueGroup>
    {
        Task<IEnumerable<QueueGroup>> GetAllAsync();
    }
}

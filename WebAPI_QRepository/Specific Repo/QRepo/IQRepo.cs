using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CallBack_Model.Model;

namespace WebAPI_QRepository.Specific_Repo.QRepo
{
    public interface IQRepo : IRepository<QueueModel>
    {
        Task<IEnumerable<QueueModel>> GetAllAsync();
    }
}

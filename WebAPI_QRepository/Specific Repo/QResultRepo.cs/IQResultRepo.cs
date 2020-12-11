using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CallBack_Model.Interface;
using CallBack_Model.Model;

namespace WebAPI_QRepository.Specific_Repo.QResultRepo.cs
{
    public interface IQResultRepo : IRepository<QResult>
    {
        Task<IEnumerable<QResult>> GetAllAsync(int QueueID);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CallBack_Model.Model;
using Microsoft.EntityFrameworkCore.Query.Internal;
using WebAPI_QRepository;
using WebAPI_QRepository.Specific_Repo;
using WebAPI_QRepository.Specific_Repo.QResultRepo.cs;

namespace WebAPI_QBusiness.QueueResult
{
    public class QResultBusiness : IQResultBusiness
    {
        private IQResultRepo _qItemRepo;

        public QResultBusiness(IUnitOfWork unitofwork)
        {
            _qItemRepo = unitofwork.QResultRepo;
        }

        public async Task<IEnumerable<QResult>> GetQResult(int QueueID)
        {
            var result = await _qItemRepo.GetAllAsync(QueueID);
            if (result == null)
                throw new NullReferenceException("Database contains no QResult by that Queue ID.");

            return result;
        }
    }
}

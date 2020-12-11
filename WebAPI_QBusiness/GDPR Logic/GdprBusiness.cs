using Gdpr_Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI_QRepository;
using WebAPI_QRepository.Specific_Repo;
using WebAPI_QRepository.Specific_Repo.GDPRepo;
using System.Threading.Tasks;

namespace WebAPI_QBusiness.GDPR_Logic
{
    public class GdprBusiness : IGdprInterface
    {
        private readonly IGdprRepo _qItemRepo;

        public GdprBusiness(IUnitOfWork unitofwork)
        {
            _qItemRepo = unitofwork.QGdprRepo;
        }

        public async Task<bool> AnonymiseMe(GdprSearchModel searchModel)
        {
            return await _qItemRepo.UpdateAsync(searchModel);
        }

        public async Task<bool> ForgetMe(GdprSearchModel searchModel)
        {
            return await _qItemRepo.DeleteAsync(searchModel.WescotRef);
        }
    }
}

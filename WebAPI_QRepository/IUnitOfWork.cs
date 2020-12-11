using WebAPI_QRepository.Specific_Repo;
using WebAPI_QRepository.Specific_Repo.CSU;
using WebAPI_QRepository.Specific_Repo.DPABreachStage1;
using WebAPI_QRepository.Specific_Repo.DPABreachStage2;
using WebAPI_QRepository.Specific_Repo.GDPRepo;
using WebAPI_QRepository.Specific_Repo.QActionsRepo;
using WebAPI_QRepository.Specific_Repo.QGroup;
using WebAPI_QRepository.Specific_Repo.QRepo;
using WebAPI_QRepository.Specific_Repo.QResultRepo.cs;

namespace WebAPI_QRepository
{
    public interface IUnitOfWork
    {
        IQueueItmRepo QueueItemRepo { get; }

        ICSURepo CSUItemRepo { get; }

        IQResultRepo QResultRepo { get; }

        IQGroupRepo QGroupRepo { get; }

        IQActionRepo QActionRepo { get; }

        IGdprRepo QGdprRepo { get; }

        IQRepo QRepo { get; }

        IBreachStage1Repo BreachStage1Repo { get; }

        IBreachStage2Repo BreachStage2Repo { get; }
    }
}

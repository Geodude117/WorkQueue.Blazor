using System.Data;
using System.Data.SqlClient;
using WebAPI_QRepository.Specific_Repo;
using WebAPI_QRepository.Specific_Repo.CSU;
using WebAPI_QRepository.Specific_Repo.DPABreachStage1;
using WebAPI_QRepository.Specific_Repo.DPABreachStage2;
using WebAPI_QRepository.Specific_Repo.GDPRepo;
using WebAPI_QRepository.Specific_Repo.QActionsRepo;
using WebAPI_QRepository.Specific_Repo.QGroup;
using WebAPI_QRepository.Specific_Repo.QRepo;
using WebAPI_QRepository.Specific_Repo.QResultBusiness.cs;
using WebAPI_QRepository.Specific_Repo.QResultRepo.cs;

namespace WebAPI_QRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly string _connection;

        /// <summary>
        /// Designed like this for DI _connection, Connection will generate new instance on call.
        /// </summary>
        /// <param name="connection"></param>
        public UnitOfWork(string connection)
        {
            _connection = connection;

            QueueItemRepo = new QueueItmRepo(connection);
            CSUItemRepo = new CSURepo(connection);
            QResultRepo = new QResultRepo(connection);
            QGroupRepo = new QGroupRepo(connection);
            QActionRepo = new QActionRepo(connection);
            QGdprRepo = new GdprRepo(connection);
            QRepo = new QRepo(connection);
            BreachStage1Repo = new BreachStage1Repo(connection);
            BreachStage2Repo = new BreachStage2Repo(connection);
        }

        public IQueueItmRepo QueueItemRepo { get; }

        public ICSURepo CSUItemRepo { get; }

        public IQResultRepo QResultRepo { get; }

        public IQGroupRepo QGroupRepo { get; }

        public IQActionRepo QActionRepo { get; }

        public IGdprRepo QGdprRepo { get; }

        public IQRepo QRepo { get; }

        public IBreachStage1Repo BreachStage1Repo {get;}

        public IBreachStage2Repo BreachStage2Repo { get; }
    }
}

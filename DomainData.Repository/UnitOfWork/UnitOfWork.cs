

using DomainData.Repository.DomainGroupRepo;
using DomainData.Repository.DomainInformationRepo;
using DomainData.Repository.DomainTypeRepo;

namespace DomainData.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly string _connection;

        public UnitOfWork(string connection)
        {
            _connection = connection;

            GroupRepo = new DomainGroupRepo.DomainGroupRepo(connection);
            InformationRepo = new DomainInformationRepo.DomainInformationRepo(connection);
            TypeRepo = new DomainTypeRepo.DomainTypeRepo(connection);
        }

     
        public IDomainGroupRepo GroupRepo { get; }

        public IDomainInformationRepo InformationRepo { get; }

        public IDomainTypeRepo TypeRepo { get; }
    }
}

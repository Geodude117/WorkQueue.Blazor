using DomainData.Repository.DomainGroupRepo;
using DomainData.Repository.DomainInformationRepo;
using DomainData.Repository.DomainTypeRepo;


namespace DomainData.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IDomainGroupRepo GroupRepo { get; }

        IDomainInformationRepo InformationRepo { get; }

        IDomainTypeRepo TypeRepo { get; }
    }
}

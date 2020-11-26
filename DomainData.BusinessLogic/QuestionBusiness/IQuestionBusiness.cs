using DomainData.Models.ViewModels;
using System.Threading.Tasks;

namespace DomainData.BusinessLogic.QuestionViewModel
{
    public interface IQuestionBusiness
    {
        Task<DomainViewModel> GetQuestionSet(int groupId);
    }
}
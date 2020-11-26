using DomainData.Models.QuestionModels;
using DomainData.Models.UIModels;
using DomainData.Models.ViewModels;
using DomainData.Repository.DomainGroupRepo;
using DomainData.Repository.DomainInformationRepo;
using DomainData.Repository.DomainTypeRepo;
using DomainData.Repository.UnitOfWork;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DomainData.BusinessLogic.QuestionViewModel
{
    public class QuestionBusiness : IQuestionBusiness
    {

        private readonly IDomainGroupRepo _groupRepo;
        private readonly IDomainInformationRepo _informationRepo;
        private readonly IDomainTypeRepo _typeRepo;

        public QuestionBusiness(IUnitOfWork unitOfWork)
        {
            _groupRepo = unitOfWork.GroupRepo;
            _informationRepo = unitOfWork.InformationRepo;
            _typeRepo = unitOfWork.TypeRepo;

        }

        public async Task<DomainViewModel> GetQuestionSet(int groupId)
        {
            DomainViewModel model = new DomainViewModel();

            var selectedDomainGroup = await _groupRepo.GetAsync(groupId);

            model.DomainGroup = selectedDomainGroup;

            var SelectedDomainInformation = await _informationRepo.GetAllByGroupIdAsync(selectedDomainGroup.Id);

            List<DomainInfoViewModel> domainInfoViewModel = new List<DomainInfoViewModel>();
            

            foreach (var domainInformation in SelectedDomainInformation)
            {
                DomainInfoViewModel infoModel = new DomainInfoViewModel
                {
                    DomainInformation = domainInformation,
                    DomainType = await _typeRepo.GetAsync(domainInformation.TypeId),
                };

                if (infoModel.DomainType.TypeName == CustomType.StringType.ToString())
                {

                    TextQuestion textQuestion = new TextQuestion
                    {
                        Text = infoModel.DomainInformation.Title,
                        Order = infoModel.DomainInformation.Order
                    };
                    infoModel.TextQuestion = textQuestion;
                }
                else if (infoModel.DomainType.TypeName == CustomType.BoolType.ToString())
                {
                    BoolQuestion boolQuestion = new BoolQuestion
                    {
                        Text = infoModel.DomainInformation.Title,
                        Order = infoModel.DomainInformation.Order
                    };
                    infoModel.BoolQuestion = boolQuestion;
                }
                else if (infoModel.DomainType.TypeName == CustomType.IntType.ToString())
                {
                    IntQuestion intQuestion = new IntQuestion
                    {
                        Text = infoModel.DomainInformation.Title,
                        Order = infoModel.DomainInformation.Order
                    };
                    infoModel.IntQuestion = intQuestion;
                }
                else if (infoModel.DomainType.TypeName == CustomType.DateTimeType.ToString())
                {
                    DateTimeQuestion dateTimeQuestion = new DateTimeQuestion
                    {
                        Text = infoModel.DomainInformation.Title,
                        Order = infoModel.DomainInformation.Order
                    };
                    infoModel.DateTimeQuestion = dateTimeQuestion;
                }

                domainInfoViewModel.Add(infoModel);
            }

            model.DomainInfoViewModels = domainInfoViewModel;


            return model;
        }
    }
}

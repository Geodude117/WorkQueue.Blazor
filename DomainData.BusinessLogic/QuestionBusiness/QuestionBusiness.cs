using DomainData.Models.QuestionModels;
using DomainData.Models.UIModels;
using DomainData.Models.ViewModels;
using DomainData.Repository.DomainGroupRepo;
using DomainData.Repository.DomainInformationRepo;
using DomainData.Repository.DomainTypeRepo;
using DomainData.Repository.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
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

            List<IDomainInfoViewModels> domainInfoViewModel = new List<IDomainInfoViewModels>();
            

            foreach (var domainInformation in SelectedDomainInformation)
            {
                DomainInfoViewModel infoModel = new DomainInfoViewModel
                {
                    DomainInformation = domainInformation,
                    DomainType = await _typeRepo.GetAsync(domainInformation.TypeId),
                };

                if (infoModel.DomainType.TypeName == CustomType.StringType.ToString())
                {

                    IQuestion textQuestion = new TextQuestion
                    {
                        Text = infoModel.DomainInformation.Title,
                        Order = infoModel.DomainInformation.Order,
                        HasValidation = infoModel.DomainInformation.HasValidation

                    };
                    infoModel.Question = textQuestion;
                }
                else if (infoModel.DomainType.TypeName == CustomType.BoolType.ToString())
                {
                    IQuestion boolQuestion = new BoolQuestion
                    {
                        Text = infoModel.DomainInformation.Title,
                        Order = infoModel.DomainInformation.Order,
                        HasValidation = infoModel.DomainInformation.HasValidation
                    };
                    infoModel.Question = boolQuestion;
                }
                else if (infoModel.DomainType.TypeName == CustomType.IntType.ToString())
                {
                    IQuestion intQuestion = new IntQuestion
                    {
                        Text = infoModel.DomainInformation.Title,
                        Order = infoModel.DomainInformation.Order,
                        HasValidation = infoModel.DomainInformation.HasValidation
                    };
                    infoModel.Question = intQuestion;
                }
                else if (infoModel.DomainType.TypeName == CustomType.DateTimeType.ToString())
                {
                    IQuestion dateTimeQuestion = new DateTimeQuestion
                    {
                        Text = infoModel.DomainInformation.Title,
                        Order = infoModel.DomainInformation.Order,
                        HasValidation = infoModel.DomainInformation.HasValidation

                    };
                    infoModel.Question = dateTimeQuestion;
                }
                else if (infoModel.DomainType.TypeName == CustomType.DropdownType.ToString())
                {
                    IQuestion dropdownQuestion = new DropdownQuestion
                    {
                        Text = infoModel.DomainInformation.Title,
                        Order = infoModel.DomainInformation.Order,
                        HasValidation = infoModel.DomainInformation.HasValidation,
                        Values = infoModel.DomainInformation.Arguments.Split(',').ToList()
                    };
                    infoModel.Question = dropdownQuestion;
                }

                domainInfoViewModel.Add(infoModel);
            }

            model.DomainInfoViewModels = domainInfoViewModel;


            return model;
        }
    }
}

using DomainData.Models.QuestionModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainData.Models.ViewModels
{
    public class DomainViewModel
    {
        public DomainViewModel()
        {
            this.DomainInfoViewModels = new List<DomainInfoViewModel>();
            this.DomainGroup = new DomainGroup();
        }
        public DomainGroup DomainGroup { get; set; }

        public List<DomainInfoViewModel> DomainInfoViewModels { get; set; }


    }


}

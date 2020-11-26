using DomainData.Models.QuestionModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainData.Models.ViewModels
{
    public class DomainInfoViewModel
    {
        public DomainInfoViewModel()
        {
        }
        public DomainInformation DomainInformation { get; set; }
        public DomainType DomainType { get; set; }

        public TextQuestion TextQuestion { get; set; }
        public BoolQuestion BoolQuestion { get; set; }
        public IntQuestion IntQuestion { get; set; }
        public DateTimeQuestion DateTimeQuestion { get; set; }


    }
}

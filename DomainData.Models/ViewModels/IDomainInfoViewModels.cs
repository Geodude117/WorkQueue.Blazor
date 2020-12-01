using DomainData.Models.QuestionModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainData.Models.ViewModels
{
    public interface IDomainInfoViewModels 
    {
        public IQuestion Question { get; set; }
        public DomainInformation DomainInformation { get; set; }
        public DomainType DomainType { get; set; }
    }
}

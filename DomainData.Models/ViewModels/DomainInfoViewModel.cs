﻿using DomainData.Models.QuestionModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainData.Models.ViewModels
{
    public class DomainInfoViewModel : IDomainInfoViewModels
    {
        public DomainInfoViewModel()
        {
        }

        public IQuestion Question { get; set; }
        public DomainInformation DomainInformation { get; set; }
        public DomainType DomainType { get; set; }

    }
}

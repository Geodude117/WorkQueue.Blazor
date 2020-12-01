﻿using DomainData.Models.QuestionModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainData.Models.ViewModels
{
    public class DomainViewModel : IDomainViewModel
    {
        public DomainViewModel()
        {
        }
        public DomainGroup DomainGroup { get; set; }

        public List<IDomainInfoViewModels> DomainInfoViewModels { get; set; }

    }


}

using System;
using System.Collections.Generic;
using System.Text;

namespace DomainData.Models.ViewModels
{
    public interface IDomainViewModel
    {
      
        public DomainGroup DomainGroup { get; set; }

        public List<IDomainInfoViewModels> DomainInfoViewModels { get; set; }

    }
}

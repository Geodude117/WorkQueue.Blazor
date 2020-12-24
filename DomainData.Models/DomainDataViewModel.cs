using System;
using System.Collections.Generic;
using System.Text;

namespace DomainData.Models
{
    public class DomainDataViewModel
    {
        public DomainGroup DomainData { get; set; }
        public IList<DomainInformation> DomainInformation { get; set; }
    }
}

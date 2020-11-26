using System;
using System.Collections.Generic;
using System.Text;

namespace DomainData.Models
{
    public class DomainGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string ExternalReferenceId { get; set; }
        public bool IsActive { get; set; }
    }
}

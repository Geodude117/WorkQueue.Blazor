using System;
using System.Collections.Generic;
using System.Text;

namespace DomainData.Models
{
    public class DomainInformation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ObjectMapping { get; set; }
        public int Order { get; set; }
        public int TypeId { get; set; }
        public int GroupId { get; set; }
    }
}

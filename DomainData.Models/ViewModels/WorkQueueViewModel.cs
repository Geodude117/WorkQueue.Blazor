using System;
using System.Collections.Generic;
using System.Text;

namespace DomainData.Models.ViewModels
{
    public class WorkQueueViewModel
    {
        public IDomainViewModel DomainViewModel { get; set; }
        public IDomainViewModel QueueItemViewModel { get; set; }


    }
}

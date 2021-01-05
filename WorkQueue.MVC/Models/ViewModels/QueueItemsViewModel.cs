using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallBack_Model.Model;

namespace WorkQueue.MVC.Models.ViewModels
{
    public class QueueItemsViewModel
    {
        public int QueueGroupID { get; set; }

        public IEnumerable<QueueItem> QitemList { get; set;  }
    }
}

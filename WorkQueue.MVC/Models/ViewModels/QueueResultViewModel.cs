using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallBack_Model.Model;

namespace WorkQueue.MVC.Models.ViewModels
{
    public class QueueResultViewModel
    {
        public QueueItem QueiQueueItem { get; set; }
        public IEnumerable<QResult> QResults { get; set;}
        public CSU_Callback CsuCallback { get; set; }
        public string IDVWeb { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string ContactNumber { get; set; }
        public int QueueGroupId { get; set; }
    }
}

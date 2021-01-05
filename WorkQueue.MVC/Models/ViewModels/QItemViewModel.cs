using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CallBack_Model.Model;

namespace WorkQueue.MVC.Models.ViewModels
{
    public class QItemViewModel: QueueItem
    {
        public string DisplayName { get; set; }

        public string CurrentPermissions { get; set; }

        public bool AddressNeeded { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }

    }
}

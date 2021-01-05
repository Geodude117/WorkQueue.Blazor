using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallBack_Model.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WorkQueue.MVC.Models.ViewModels
{
    public class CallBackDetailsViewModel
    {
        public QItemViewModel QItemViewModel { get; set; }

        public CSU_Callback CSUItem { get; set; }

        public string ActionType { get; set; }

        public IEnumerable<SelectListItem> CSURelationshipOptions()
        {
            return RealtionshipOptions.CSURelationshipOptions();
        }

    }
}

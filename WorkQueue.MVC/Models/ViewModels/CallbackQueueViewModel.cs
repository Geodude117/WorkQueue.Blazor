using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CallBack_Model.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorkQueue.MVC.Models;

namespace WorkQueue.MVC.Models.ViewModels
{
    public class CallbackQueueViewModel
    {
        public QueueItem QItem { get; set; }
        public CSU_Callback CSUItem { get; set; }
        public int? Wesref { get; set; }
    
        public IEnumerable<SelectListItem> CSURelationshipOptions()
        {
            return RealtionshipOptions.CSURelationshipOptions();
        }

        //Hold all the queue action results relating to a Callback
        public IEnumerable<QResult> QResults { get; set; }

        public string UserPermission { get; set; }

        public SelectList ContactNumbers { get; set; }

        public List<string> ContactNumbersString { get; set; }

        public string SelectedContactNumber { get; set; }

        public bool FromAgentDesktop { get; set; }

        public int ReturnID { get; set; }
    }

}

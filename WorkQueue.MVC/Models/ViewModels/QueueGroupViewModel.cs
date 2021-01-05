using System.Collections.Generic;
using CallBack_Model.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WorkQueue.MVC.Models.ViewModels
{
    public class QueueGroupViewModel
    {
        public int QueueGroupID { set; get; }
        public List<QueueGroup> QueueGroups { set; get; }

        public int SelectedGroupId { set; get; }
        public SelectList QueueGroupList { get; set; }
    }
}

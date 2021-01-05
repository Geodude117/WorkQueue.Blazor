using System.Threading.Tasks;
using CallBack_Model.Model;
using Microsoft.AspNetCore.Mvc;

namespace WorkQueue.MVC.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index(int QueueID)
        {

            return View(new SearchParameters()
            {
                QueueGroup = QueueID
            });
        }

        #region "Search Action Methods"

        public async Task<ActionResult> SearchBy(SearchParameters model, [FromServices] IHttpConnectionFactory<QueueItem> HttpClientConnection, string ActiveCallbackCheckedBox)
        {

           
            if (model.Action_AgentId != null)
            {
                model.Action_AgentId = "WESCOT\\" + model.Action_AgentId;
            }
            else if (model.Raise_AgentId!= null)
            {
                model.Raise_AgentId = "WESCOT\\" + model.Raise_AgentId;
            }
            else if (model.EndDate.HasValue)
            {
                model.EndDate = model.EndDate.Value.Date;
                model.EndDate = model.EndDate.Value.AddHours(23);
                model.EndDate = model.EndDate.Value.AddMinutes(59);
                model.EndDate = model.EndDate.Value.AddSeconds(59);
            }

            var queueItems = await HttpClientConnection.GetSearchAsync(model);
            return PartialView("~/Views/WorkQueue/FullDetails.cshtml", queueItems);
        }
  

        #endregion

    }




    
}
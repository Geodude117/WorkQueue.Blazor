using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using CallBack_Model.Model;
using DebtManager3AccountModels.Models;
using DebtManager3NotesModels.Models;
using Microsoft.AspNetCore.Mvc;
using WorkQueue.MVC.Helpers;
using WorkQueue.MVC.Models.ViewModels;
using Microsoft.Extensions.Configuration;

namespace WorkQueue.MVC.Controllers
{
    public class CallbackController : Controller
    {
        private IHttpConnectionFactory<CSU_Callback> HttpClientConnectionCallback;
        private IHttpConnectionFactory<QueueItem> HttpClientConnectionQueueItem;

        public CallbackController([FromServices] IHttpConnectionFactory<CSU_Callback> HttpClientConnectionCSU,
            [FromServices] IHttpConnectionFactory<QueueItem> HttpClientConnectionQItem)
        {
            HttpClientConnectionQueueItem = HttpClientConnectionQItem;
            HttpClientConnectionCallback = HttpClientConnectionCSU;
        }

        /// <summary>
        /// This Action is enclosed in a form which takes all the input fields for a callback update
        /// Sends a put request to the WeB api with the updated callback model as a parameter
        /// </summary>
        public async Task<ActionResult> Save(CallbackQueueViewModel model)
        {
            model.CSUItem.ReasonForTransfer = "Null";
            var result = await HttpClientConnectionCallback.PutAsync(model.CSUItem);

            if (result)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }


        #region "Locking Logic"
        public async Task<ActionResult> Lock(int id)
        {
            //Lock Queue Item
            QueueItem queueItem = (await HttpClientConnectionQueueItem.GetSearchAsync(new SearchParameters() { QueueItemID = id })).FirstOrDefault();

            if (queueItem.Islocked.Value && queueItem.LockedBy != User.Identity.Name)
            {
                return Json(new { success = false });
            }
            else
            {
                queueItem.Islocked = true;
                queueItem.LockedBy = User.Identity.Name;
                queueItem.LockTime = DateTime.Now;
                var result = await HttpClientConnectionQueueItem.PutAsync(queueItem);
                return Json(new { success = true });
            }
        }

        public async void Unlock(int id)
        {
            var itemobject = (await HttpClientConnectionQueueItem.GetSearchAsync(new SearchParameters() { QueueItemID = id })).First();
            itemobject.Islocked = null;
            itemobject.LockedBy = null;
            itemobject.LockTime = null;
            var result = await HttpClientConnectionQueueItem.PutAsync(itemobject);
        }

        public async Task<ActionResult> CheckLock(int id)
        {
            //Lock Queue Item
            QueueItem queueItem = (await HttpClientConnectionQueueItem.GetSearchAsync(new SearchParameters() { QueueItemID = id })).FirstOrDefault();

            if (queueItem.Islocked.Value && queueItem.LockedBy != User.Identity.Name)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        #endregion
        
        /// <summary>
        /// Populates a table if callback information for a Queue Item ID
        /// A partial view that holds a CallbackQueueViewModel type which contains multiple properties including a list of Queue results
        /// <summary>
        public async Task<ActionResult> Details(QItemViewModel QitemViewModel, [FromServices] IHttpConnectionFactory<AccountModel> HttpClientConnectionAccountHolder,
            [FromServices]IUserPermissionLogic userPermissionLogic)
        {
            CSU_Callback csuCallback = await HttpClientConnectionCallback.GetAsync(QitemViewModel.QueueItemID.Value);
            QitemViewModel.CurrentPermissions = userPermissionLogic.GetUserPermissions(User.Identity.Name).ToString();
            string acttype = StringType(QitemViewModel.QueueID);

            if (acttype == "CSU Letter" || acttype == "CSU Review")
            {
                AccountModel accountFull = await HttpClientConnectionAccountHolder.GetAsync(csuCallback.WescotRef);
                QitemViewModel.AddressLine1 = accountFull.Contact.addressLine1?.TrimEnd();
                QitemViewModel.AddressLine2 = accountFull.Contact.addressLine2?.TrimEnd();
                QitemViewModel.AddressLine3 = accountFull.Contact.addressLine3?.TrimEnd();
            }
            
            CallBackDetailsViewModel queueResultView = new CallBackDetailsViewModel
            {
                CSUItem = csuCallback,
                QItemViewModel = QitemViewModel,
                ActionType = acttype
            };
            
            return PartialView(queueResultView);
        }

       
        /// <summary>
        /// Old method which was loacted in the side nav bar which takes you to a new page to process the callback
        /// Acion retireves all the action results 
        /// </summary>
        public async Task<ActionResult> Action(int id, string contactNumber, string addressLine1, string addressLine2, string addressLine3,
            [FromServices] IHttpConnectionFactory<QResult> HttpClientConnectionQueueResultGetAll, [FromServices]IConfiguration serviceConfig)
        {
            QueueItem queueItem = (await HttpClientConnectionQueueItem.GetSearchAsync(new SearchParameters() { QueueItemID = id })).FirstOrDefault();

            queueItem.Islocked = true;
            queueItem.LockedBy = User.Identity.Name;
            queueItem.LockTime = DateTime.Now;
            var result = await HttpClientConnectionQueueItem.PutAsync(queueItem);

            QueueResultViewModel queueResultView = new QueueResultViewModel
            {
                QResults = await HttpClientConnectionQueueResultGetAll.GetAllAsync(queueItem.QueueID),
                QueiQueueItem = queueItem,
                IDVWeb = serviceConfig.GetValue<string>("WebRedirects:IDV"),
                AddressLine1 = addressLine1,
                AddressLine2 = addressLine2,
                AddressLine3 = addressLine3,
                ContactNumber = contactNumber
            };

            ViewBag.QueueName = StringType(queueItem.QueueID);
           
            return View(queueResultView);
        }
       
        public async void QueueResultButtonClick(int queueResultID, int queueItemID, string queueItemName,
            [FromServices] IHttpConnectionFactory<QItemHolder> HttpClientConnectionQItemHolder)
        {
           
            QItemHolder qItem = new QItemHolder()
            {
                queueItem = (await HttpClientConnectionQueueItem.GetSearchAsync(new SearchParameters() { QueueItemID = queueItemID })).FirstOrDefault(),
                TModel = await HttpClientConnectionCallback.GetAsync(queueItemID)
            };

            //This fills in the data needed when a callback is created
            qItem.queueItem.CompletedDate = DateTime.Now;
            qItem.queueItem.CompletedBy = qItem.queueItem.LockedBy;

            //Send a request to the Web API a put complete which executes a specific action to the callback
            var x = HttpClientConnectionQItemHolder.PutAsync(queueResultID, qItem);
        }
        
        public async Task<ActionResult> GetAccountNotes(int AccountNumber, [FromServices] IHttpConnectionFactory<NotesViewModel> HttpClientConnectionNotesHolder)
        {
            string endDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            string startDate = DateTime.Now.AddDays(-60).ToString("yyyy-MM-dd");

            var results = await HttpClientConnectionNotesHolder.GetSearchAsync(new List<string>() { AccountNumber.ToString(), startDate, endDate });

            return PartialView(results);

        }

        private string StringType(int ID)
        {
            var stringType = string.Empty;
            switch (ID)
            {
                case 1:
                    stringType = "1st Callback";
                    break;
                case 2:
                    stringType = "2nd Callback";
                    break;
                case 3:
                    stringType = "CSU Letter";
                    break;
                case 4:
                    stringType = "CSU Review";
                    break;
            }
            return stringType;
        }
    }
}
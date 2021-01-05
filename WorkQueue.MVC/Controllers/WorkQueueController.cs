using CallBack_Model.Model;
using DebtManager3AccountModels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WorkQueue.MVC.Helpers;
using WorkQueue.MVC.Models.ViewModels;

namespace WorkQueue.MVC.Controllers
{

    public class WorkQueueController : Controller
    {

        /// <summary>
        /// This is the first page that is loaded, Ask the Web API for all the groups and display them in a dropdown box
        /// </summary>
        /// 
        public async Task<ActionResult> Index(QueueGroupViewModel model, [FromServices] IHttpConnectionFactory<QueueGroup> httpClientConnection, [FromServices]IUserPermissionLogic userPermissionLogic)
        {
            UserPermissions userPermissions = userPermissionLogic.GetUserPermissions(User.Identity.Name);

            if (userPermissions != UserPermissions.Public || userPermissions != UserPermissions.CreateUser)
            {
                //Returns all of the queue groups
                var result = await httpClientConnection.GetAllAsync();

                var queueGroups = result.ToList();
                model.QueueGroupList = new SelectList(queueGroups, "QueueGroupID", "Name");

                return View(model);
            }
            else
            {
                return View(model);
            }

        }


        /// <summary>
        /// Calls the Web API asks for all the queue items which relate to the selected group picked by the user from the dropdown box s
        /// </summary>
        public async Task<ActionResult> Details(QueueGroupViewModel model, [FromServices] IHttpConnectionFactory<QueueItem> httpClientConnection, [FromServices]IUserPermissionLogic userPermissionLogic)
        {
            QueueItemsViewModel viewModel = new QueueItemsViewModel()
            {
                QueueGroupID = model.SelectedGroupId,
                QitemList = await httpClientConnection.GetAllAsync(model.SelectedGroupId)
            };
            
            return PartialView(viewModel);
        }
        
        /// <summary>
        /// This Action gets the Wescot ref and ustomer name from DM if it exist and displays them as readonly in the view
        /// Retruns a view for creation for a callback
        /// </summary>
        public async Task<ActionResult> Create(int? wescotRef, string CustomerName, string TelphoneNumber,string MobileNumber, 
            string EmployerTel,int QueueID, [FromServices]IUserPermissionLogic userPermissionLogic, [FromServices]IHttpConnectionFactory<QueueGroup> QueueGrouphttpclient)
        {
            var queuegroups = (await QueueGrouphttpclient.GetAllAsync()).Where(x => x.QueueGroupID == QueueID).FirstOrDefault();

            CallbackQueueViewModel model = new CallbackQueueViewModel()
            {
                CSUItem = new CSU_Callback
                {
                    WescotRef = wescotRef.GetValueOrDefault(),
                    DateForCallback = DateTime.Now.AddDays(2)
                },
                QItem = new QueueItem()
                {
                    CustomerName = CustomerName,
                    CreatedBy = User.Identity.Name,
                    WescotRef = wescotRef.GetValueOrDefault(),
                    QueueID = queuegroups.DefaultQueueID
                },
                Wesref = wescotRef,
                UserPermission = userPermissionLogic.GetUserPermissions(User.Identity.Name).ToString(),
                ContactNumbers = ContactNumberCreation(TelphoneNumber, MobileNumber, EmployerTel),
                ReturnID = QueueID
            };

            if (CustomerName != null || wescotRef != null 
               || TelphoneNumber != null || MobileNumber != null || EmployerTel != null)
            {
                model.FromAgentDesktop = true;
            }

            return View(model);
        }

        private SelectList ContactNumberCreation(string HomeNum, string MobileNum, string EmployerNum)
        {
            var x = new List<string>();
            
            if (!(string.IsNullOrEmpty(HomeNum) || HomeNum == ".") && ulong.TryParse(StringHelper.ReplaceWhitespace(HomeNum, ""), out ulong xint))
                x.Add(HomeNum);

            if (!(string.IsNullOrEmpty(MobileNum) || MobileNum == ".") && ulong.TryParse(StringHelper.ReplaceWhitespace(MobileNum, ""), out xint))
                x.Add(MobileNum);

            if (!(string.IsNullOrEmpty(EmployerNum) || EmployerNum == ".") && ulong.TryParse(StringHelper.ReplaceWhitespace(EmployerNum, ""), out xint))
                x.Add(EmployerNum);
            return x.Count == 0 ? null : new SelectList(x.Distinct());
        }

        /// <summary>
        /// This action sends the Web API a QueueItemHolder as a POST which consist of a queueitem and callback
        /// Sets the parameters that are needed in the callback/queueitem models
        /// </summary>
        public async Task<ActionResult> CreateModelQueueItm(CallbackQueueViewModel model, [FromServices] IHttpConnectionFactory<QItemHolder> httpClientConnection)
        {
                model.QItem.CreatedDate = DateTime.Now;
                model.QItem.WescotRef = model.CSUItem.WescotRef;
                var Nameofthecaller = model.QItem.CustomerName;
                model.QItem.CustomerName = model.CSUItem.NameOfcaller;
                model.QItem.DueDate = model.CSUItem.DateForCallback;
                model.QItem.Summary = "Time to Avoid: " + model.CSUItem.TimeToAvoid + ". Reason for Callback: " + model.CSUItem.ReasonForCallback;
                QItemHolder qItemHolder = new QItemHolder
                {
                    queueItem = model.QItem,
                    TModel = model.CSUItem
                };
                var x = JsonConvert.SerializeObject(qItemHolder);
                var result = await httpClientConnection.PostAsync(qItemHolder);

                // has the operation been successful?
                if (result)
                {
                    // return an ok result and replace the view with an empty one
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                    //if model.Wesref is null then have come from main site else via agent deskotp
                    CallbackQueueViewModel newModel = new CallbackQueueViewModel
                    {
                        CSUItem = new CSU_Callback()
                        {
                            WescotRef = model.Wesref != null ? model.CSUItem.WescotRef : 0,
                            ReasonForCallback = string.Empty
                        },
                        QItem = new QueueItem(),
                        ContactNumbers = model.ContactNumbersString == null ? null:  new SelectList(model.ContactNumbersString),
                        Wesref = model.Wesref,
                        UserPermission =  model.UserPermission
                    };

                    if (model.FromAgentDesktop)
                    {
                        newModel.QItem.CustomerName = Nameofthecaller;
                    }
                    
                    return PartialView("CreatePartial", newModel);
                }
                else
                {
                    // return error response
                    return new BadRequestResult();
                }

            }

        public async Task<IActionResult> VerifyWorkQueue(CallbackQueueViewModel model, [FromServices]
        IHttpConnectionFactory<AccountModel> HttpClientConnectionAccountHolder)
        {
            try
            {
                var accountFull = await HttpClientConnectionAccountHolder.GetAsync(model.CSUItem.WescotRef);

                if (accountFull.AccountNumber == 0)
                {
                    return Json($"This reference {model.CSUItem.WescotRef} is invalid.");
                }
            }
            catch (Exception Ex)
            {
                return Json($"This reference is invalid.");
            }
            return Json(true);
        }
    }
}
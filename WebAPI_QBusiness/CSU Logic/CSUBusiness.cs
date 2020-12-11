using CallBack_Model.Interface;
using CallBack_Model.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebAPI_QRepository;
using WebAPI_QRepository.Specific_Repo.CSU;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebAPI_QBusiness.QueueActions_Logic;

namespace WebAPI_QBusiness
{
    public class CSUBusiness : ICSUBusiness
    {
        private readonly ICSURepo _csuRepo;
        private readonly IQueueBusiness _queueBusiness;
        private readonly IQActionBusiness _qActionsBusiness; 

        public CSUBusiness(IUnitOfWork unitOfWork, IQueueBusiness qBusiness, IQActionBusiness qActions)
        {
            _queueBusiness = qBusiness;
            _csuRepo = unitOfWork.CSUItemRepo;
            _qActionsBusiness = qActions; 
        }

        public async Task<CSU_Callback> GetCSUItem(int QueueItemID)
        {
            return await _csuRepo.GetAsync(QueueItemID);
        }

        public async Task<bool> Post(QueueItem QItem, CSU_Callback CSUitem)
        {
            if (CSUitem.DateForCallback != QItem.DueDate || CSUitem.DateForCallback < DateTime.Today)
                return false;
            return await CreateCallback(QItem, CSUitem);
        }

        public async Task<bool> PutCSUItemEdit(CSU_Callback CSUitem)
        {
            if (CSUitem.CompareTo(await _csuRepo.GetAsync(CSUitem.QueueItemID.Value)) < 0)
                return false;
            return await _csuRepo.UpdateAsync(CSUitem);
        }

        public async Task<bool> PutItemCompleted(int QueueResultID, QueueItem QItem, CSU_Callback CSUitem, string noteWebApiCon)
        {
            //could implement deep copy, but these fields need to be cleared when creating a callback 
            //to keep a copy added here.

            var completeDate = QItem.CompletedDate;
            var completedBy = QItem.CompletedBy;

            QueueItem newQiItem = new QueueItem()
            {
                WescotRef = QItem.WescotRef, CompletedDate = QItem.CompletedDate, CompletedBy = QItem.CompletedBy,
                CreatedBy = QItem.CompletedBy,QueueID = 0, Summary = QItem.Summary,
                CreatedDate = DateTime.Now, CustomerName = QItem.CustomerName, ParentQueueItemID = QItem.QueueItemID
            };
            CSU_Callback newCSuitem = new CSU_Callback()
            {
                WescotRef = CSUitem.WescotRef, NameOfcaller = CSUitem.NameOfcaller, Relationship = CSUitem.Relationship,
                ContactNumber = CSUitem.ContactNumber, TimeToAvoid = CSUitem.TimeToAvoid, ReasonForCallback = CSUitem.ReasonForCallback,
                ReasonForTransfer = CSUitem.ReasonForTransfer, HealthIssue = CSUitem.HealthIssue
            };
            List<Task<bool>> TaskList = new List<Task<bool>>
            {
                _csuRepo.UpdateAsync(CSUitem),
                _queueBusiness.Put_QueueItemComplete(QItem)
            };
            var results = await _qActionsBusiness.GetAllActions(QueueResultID);

            foreach (QueueAction x in results)
            {
                switch (x.Action)
                {
                    case ActionType.CreateCallback:
                        TaskList.Add(CreateCallbackDateChange(x.ActionCode, x.DefaultDays, newQiItem, newCSuitem, QItem.QueueItemID));
                        break;
                    case ActionType.DiaryNote:
                        TaskList.Add(CreateBasicDiaryNote(x.ActionCode, newQiItem.WescotRef ,completeDate.Value,completedBy, noteWebApiCon));
                        break;
                    case ActionType.CreateCSULetter:
                        TaskList.Add(CreateCallbackDateChange(x.ActionCode, x.DefaultDays, newQiItem, newCSuitem, QItem.QueueItemID));
                        break;
                }
            }
            
            var result = TaskSummary.TaskBoolSummary(await Task.WhenAll(TaskList));
            return result;
        }

        public async Task<bool> DeleteCSUItem(int QueueItemID)
        {
            var result = await _csuRepo.GetAsync(QueueItemID);
            if (result.QueueItemID == null)
                throw new ArgumentNullException("Queue item identifer was not set.");

            return await _queueBusiness.Delete_QueueItem(result.QueueItemID.Value) ? await _csuRepo.DeleteAsync(QueueItemID) : false;
        }

        private async Task<bool> CreateBasicDiaryNote(string DiaryCode, int WescotRef, DateTime completeDate, string completedBy, string noteWebApiCon)
        {
            if (completeDate == null)
                throw new ArgumentException("Date time completed has not been set.");

            DiaryRecord diaryRecord = new DiaryRecord()
            {
                AccountNumber = WescotRef,
                DiaryCode = DiaryCode,
                AgentName = completedBy,
                PostedDateTime = completeDate,
                DiaryDescription = "Csu: " + DiaryCode
            };

            return await SendToDm3(diaryRecord, noteWebApiCon);
        }

        private async Task<bool> SendToDm3(DiaryRecord diaryRecord, string noteWebApiCon)
        {
            HttpClient httpClient = new HttpClient(new HttpClientHandler()
            {
                UseDefaultCredentials = true,
                PreAuthenticate = true
            });
            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await httpClient.SendAsync(new
                    HttpRequestMessage(HttpMethod.Post, new Uri(string.Format(noteWebApiCon, diaryRecord.AccountNumber)))
                    { Content = new StringContent(JsonConvert.SerializeObject(diaryRecord),
                        Encoding.UTF8, "application/json")}
                );
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("Failed to send diary note to note web API.",ex);
            }

            return httpResponse.IsSuccessStatusCode;
        }

        private async Task<bool> CreateCallbackDateChange(string QueueResultID, int DateTimeAdjust, QueueItem QItem, CSU_Callback CSUitem, int? ParentID)
        {
            if (!int.TryParse(QueueResultID, out int SecondQueueID))
                throw new DataException("Expected a interger in action code: as action result is stored under ActionType.CreateCallback.");
            
            QItem.QueueID = SecondQueueID;

            QItem.CreatedDate = DateTime.Now;
            var temp = QItem.CreatedDate;

            var tempresult = temp.AddDays(DateTimeAdjust);
            var date = tempresult.Date;

            CSUitem.DateForCallback = date;
            QItem.DueDate = date;
            
            CSUitem.QueueItemID = null;
            CSUitem.ID = null;

            QItem.ParentQueueItemID = ParentID;
            QItem.CreatedBy = QItem.CompletedBy;
            QItem.QueueItemID = null;
            QItem.CompletedBy = null;
            QItem.CompletedDate = null;
            
            return await CreateCallback(QItem, CSUitem);
        }

        private async Task<bool> CreateCallback(QueueItem QItem, CSU_Callback CSUitem)
        {
            var queueItemresponse = await _queueBusiness.Post_QueueItem(QItem);

            if (queueItemresponse == null)
                return false;

            CSUitem.QueueItemID = queueItemresponse;

            return await _csuRepo.InsertAsync(CSUitem) != null;
        }
    }
}

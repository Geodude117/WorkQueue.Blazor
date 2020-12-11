using CallBack_Model.Model;
using DomainData.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WorkQueue.Blazor.Helpers;

namespace WorkQueue.Blazor.Data
{
    public class CSUCallbackService
    {
        private readonly IHttpConnectionFactory<QItemHolder> _httpClientConnectionQItemHolder;
        private readonly IHttpConnectionFactory<CSU_Callback> _httpClientConnectionCsuCallback;
        private readonly IHttpConnectionFactory<QueueItem> _httpClientConnectionQueueItem;

        private IConfiguration _config;
        private readonly string _queueItemGroupId;

        private readonly CustomMapper _customMapper;

        public CSUCallbackService([FromServices] IHttpConnectionFactory<QItemHolder> httpClientConnectionQItemHolder,
            [FromServices] IHttpConnectionFactory<CSU_Callback> httpClientConnectionCsuCallback,
            [FromServices] IHttpConnectionFactory<QueueItem> httpClientConnectionQueueItem,
            IConfiguration config,
            [FromServices] CustomMapper customMapper)
        {
            _config = config;
            _queueItemGroupId = _config.GetValue<string>("QueueItemGroupId");

            _httpClientConnectionQItemHolder = httpClientConnectionQItemHolder;
            _httpClientConnectionCsuCallback = httpClientConnectionCsuCallback;
            _httpClientConnectionQueueItem = httpClientConnectionQueueItem;

            _customMapper = customMapper;
        }

        public async Task<bool> PostCSU(WorkQueueViewModel model, string userName)
        {
            model.QueueItemViewModel = await GetQuestionSet(_queueItemGroupId);

            bool result = false;
            try
            {

                var csuCallbackItem = _customMapper.Map(model.DomainViewModel.DomainInfoViewModels, model.DomainViewModel.DomainGroup.ClassMapping);
                var queueItem = _customMapper.Map(model.QueueItemViewModel.DomainInfoViewModels, model.QueueItemViewModel.DomainGroup.ClassMapping);

                queueItem = _customMapper.MapProperty(queueItem, "CreatedDate", DateTime.Now);
                queueItem = _customMapper.MapProperty(queueItem, "CreatedBy", userName);
                queueItem = _customMapper.MapProperty(queueItem, "QueueID", int.Parse(model.DomainViewModel.DomainGroup.ExternalReferenceId));
                queueItem = _customMapper.MapProperty(queueItem, "QueueGroupID", int.Parse(model.QueueItemViewModel.DomainGroup.ExternalReferenceId));
                queueItem = _customMapper.MapProperty(queueItem, "CustomerName", _customMapper.GetProperty(csuCallbackItem, "NameOfcaller"));
                queueItem = _customMapper.MapProperty(queueItem, "WescotRef", _customMapper.GetProperty(csuCallbackItem, "WescotRef"));
                queueItem = _customMapper.MapProperty(queueItem, "Summary", _customMapper.GetProperty(csuCallbackItem, "ReasonForCallback"));
                queueItem = _customMapper.MapProperty(queueItem, "DueDate", _customMapper.GetProperty(csuCallbackItem, "DateForCallback"));

                csuCallbackItem = _customMapper.MapProperty(csuCallbackItem, "ReasonForTransfer", "NULL");

                QItemHolder qItemHolder = new QItemHolder
                {
                    queueItem = (QueueItem)queueItem,
                    TModel = csuCallbackItem
                };

                result = await _httpClientConnectionQItemHolder.PostAsync(qItemHolder);

            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }

            return result;
        }

        public async Task<bool> PostCompleteCSU(int queueResultId, int queueItemId)
        {
            QItemHolder qItem = new QItemHolder()
            {
                queueItem = (await _httpClientConnectionQueueItem.GetSearchAsync(new SearchParameters() { QueueItemID = queueItemId })).FirstOrDefault(),
                TModel = await _httpClientConnectionCsuCallback.GetAsync(queueItemId)
            };

            //This fills in the data needed when a callback is created
            qItem.queueItem.CompletedDate = DateTime.Now;
            qItem.queueItem.CompletedBy = "Geo";

            var result = await _httpClientConnectionQItemHolder.PutAsync(queueResultId, qItem);

            return result;
        }

        //public async Task<CSU_Callback> MapCSU(WorkQueueViewModel model, string userName)
        //{
        //    model.QueueItemViewModel = await GetQuestionSet(_queueItemGroupId);

        //    CustomMapper _customMapper = new CustomMapper();

        //    var csuCallbackItem = (CSU_Callback)_customMapper.Map(model.DomainViewModel.DomainInfoViewModels, model.DomainViewModel.DomainGroup.ClassMapping);

        //    csuCallbackItem = (CSU_Callback)_customMapper.MapProperty(csuCallbackItem, "ReasonForTransfer", "NULL");

        //    return csuCallbackItem;
        //}

        public async Task<List<IDomainInfoViewModels>> Get(int Id, List<IDomainInfoViewModels> domainInfoViewModels)
        {
            var result = await _httpClientConnectionCsuCallback.GetAsync(Id);

            var mappedList = _customMapper.ReMapItemToDynamicList(domainInfoViewModels, result);

            return mappedList;
        }

        public async Task<CSU_Callback> GetCSUCallback (int Id)
        {
            var result = await _httpClientConnectionCsuCallback.GetAsync(Id);

            return result;
        }

        public async Task<IDomainViewModel> GetQuestionSet(string Id)
        {
            DomainViewModel result = null;
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetStringAsync("http://localhost:52388/api/Question/" + Id);

                    result = JsonConvert.DeserializeObject<DomainViewModel>(response, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Objects
                    });
                }
                catch (Exception ex)
                {
                    var x = ex.Message;
                    return result;
                }
            }

            return result;
        }

    }
}

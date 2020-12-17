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
        private readonly IHttpConnectionFactory<DomainViewModel> _httpClientConnectionDomainViewModel;

        private IConfiguration _config;
        private readonly string _queueItemGroupId;

        private readonly CustomMapper _customMapper;

        public CSUCallbackService([FromServices] IHttpConnectionFactory<QItemHolder> httpClientConnectionQItemHolder,
            [FromServices] IHttpConnectionFactory<CSU_Callback> httpClientConnectionCsuCallback,
            [FromServices] IHttpConnectionFactory<QueueItem> httpClientConnectionQueueItem,
            [FromServices] IHttpConnectionFactory<DomainViewModel> httpClientConnectionDomainViewModel,
            [FromServices] CustomMapper customMapper,
            IConfiguration config)
        {
            _httpClientConnectionQItemHolder = httpClientConnectionQItemHolder;
            _httpClientConnectionCsuCallback = httpClientConnectionCsuCallback;
            _httpClientConnectionQueueItem = httpClientConnectionQueueItem;
            _httpClientConnectionDomainViewModel = httpClientConnectionDomainViewModel;
            _customMapper = customMapper;

            _config = config;
            _queueItemGroupId = _config.GetValue<string>("QueueItemGroupId");
        }

        public async Task<bool> PostCSU(WorkQueueViewModel model, string userName)
        {
            model.QueueItemViewModel = await _httpClientConnectionDomainViewModel.GetAsync(int.Parse(_queueItemGroupId));

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
    }
}

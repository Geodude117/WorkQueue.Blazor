using CallBack_Model.Model;
using DomainData.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WorkQueue.Blazor.Helpers;

namespace WorkQueue.Blazor.Data
{
    public class QueueItemService
    {
        private readonly IHttpConnectionFactory<QueueItem> _httpClientConnectionQueueItem;
        private readonly IHttpConnectionFactory<DomainViewModel> _httpClientConnectionDomainViewModel;

        private IConfiguration _config;
        private readonly string _queueItemGroupId;
        private readonly CustomMapper _customMapper;

        public QueueItemService([FromServices] IHttpConnectionFactory<QueueItem> httpClientConnectionQueueItem,
            [FromServices] IHttpConnectionFactory<DomainViewModel> httpClientConnectionDomainViewModel,
            IConfiguration config,
            [FromServices] CustomMapper customMapper)
        {
            _httpClientConnectionQueueItem = httpClientConnectionQueueItem;
            _config = config;
            _queueItemGroupId = _config.GetValue<string>("QueueItemGroupId");

            _customMapper = customMapper;
        }

        public async Task<QueueItem[]> GetQueueItems(int QueueGroup)
        {
            var result = await _httpClientConnectionQueueItem.GetAllAsync(QueueGroup);
            return result.ToArray();
        }

         public async Task<QueueItem> GetQueueItem(int QueueItemID)
        {
            QueueItem result = (await _httpClientConnectionQueueItem.GetSearchAsync(new SearchParameters() { QueueItemID = QueueItemID })).FirstOrDefault();
            return result;
        }

        public async Task<QueueItem> MapQueueItem(WorkQueueViewModel model, string userName)
        {
            model.QueueItemViewModel = await _httpClientConnectionDomainViewModel.GetAsync(int.Parse(_queueItemGroupId));

            var csuCallbackItem = _customMapper.Map(model.DomainViewModel.DomainInfoViewModels, model.DomainViewModel.DomainGroup.ClassMapping);
            var queueItem = (QueueItem)_customMapper.Map(model.QueueItemViewModel.DomainInfoViewModels, model.QueueItemViewModel.DomainGroup.ClassMapping);

            queueItem = (QueueItem)_customMapper.MapProperty(queueItem, "CreatedDate", DateTime.Now);
            queueItem = (QueueItem)_customMapper.MapProperty(queueItem, "CreatedBy", userName);
            queueItem = (QueueItem)_customMapper.MapProperty(queueItem, "QueueID", int.Parse(model.DomainViewModel.DomainGroup.ExternalReferenceId));
            queueItem = (QueueItem)_customMapper.MapProperty(queueItem, "QueueGroupID", int.Parse(model.QueueItemViewModel.DomainGroup.ExternalReferenceId));
            queueItem = (QueueItem)_customMapper.MapProperty(queueItem, "CustomerName", _customMapper.GetProperty(csuCallbackItem, "NameOfcaller"));
            queueItem = (QueueItem)_customMapper.MapProperty(queueItem, "WescotRef", _customMapper.GetProperty(csuCallbackItem, "WescotRef"));
            queueItem = (QueueItem)_customMapper.MapProperty(queueItem, "Summary", _customMapper.GetProperty(csuCallbackItem, "ReasonForCallback"));
            queueItem = (QueueItem)_customMapper.MapProperty(queueItem, "DueDate", _customMapper.GetProperty(csuCallbackItem, "DateForCallback"));

            return queueItem;
        }
    }
}

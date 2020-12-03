using CallBack_Model.Model;
using DomainData.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WorkQueue.Blazor.Helpers;

namespace WorkQueue.Blazor.Data
{
    public class CSUCallbackService
    {
        private readonly IHttpConnectionFactory<QItemHolder> _httpClientConnectionQItemHolder;
        private readonly IHttpConnectionFactory<CSU_Callback> _httpClientConnectionCsuCallback;

        private IConfiguration _config;
        private readonly string _queueItemGroupId;

        private readonly CustomMapper _customMapper;

        public CSUCallbackService([FromServices] IHttpConnectionFactory<QItemHolder> httpClientConnection,
            [FromServices] IHttpConnectionFactory<CSU_Callback> httpClientConnection2,
            IConfiguration config,
            [FromServices] CustomMapper customMapper)
        {
            _config = config;
            _queueItemGroupId = _config.GetValue<string>("QueueItemGroupId");

            _httpClientConnectionQItemHolder = httpClientConnection;
            _httpClientConnectionCsuCallback = httpClientConnection2;

            _customMapper = customMapper;
        }

        public async Task<bool> PostCSU(WorkQueueViewModel model, string userName)
        {
            model.QueueItemViewModel = await GetQuestionSet(_queueItemGroupId);

            bool result = false;
            try
            {
                CustomMapper _customMapper = new CustomMapper();

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

        public async Task<List<IDomainInfoViewModels>> Get(int Id, List<IDomainInfoViewModels> domainInfoViewModels)
        {
            var result = await _httpClientConnectionCsuCallback.GetAsync(Id);

            var mappedList = _customMapper.ReMapItemToDynamicList(domainInfoViewModels, result);

            return mappedList;
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

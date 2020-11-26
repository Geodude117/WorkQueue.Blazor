using CallBack_Model.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkQueue.Blazor.Helpers;

namespace WorkQueue.Blazor.Data
{
    public class QueueItemService
    {
        private readonly IHttpConnectionFactory<QueueItem> _httpClient;

        public QueueItemService([FromServices] IHttpConnectionFactory<QueueItem> httpClientConnection)
        {
            _httpClient = httpClientConnection;

        }

        public async Task<QueueItem[]> GetQueueItems(int QueueGroup)
        {
            var result = await _httpClient.GetAllAsync(QueueGroup);
            return result.ToArray();
        }

        public async Task<bool> Post(QueueItem queueItem)
        {
            var result = await _httpClient.PostAsync(queueItem);
            return result;
        }
    }
}

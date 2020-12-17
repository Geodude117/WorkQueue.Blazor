using CallBack_Model.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkQueue.Blazor.Helpers;

namespace WorkQueue.Blazor.Data
{
    public class QueueService
    {
        private IHttpConnectionFactory<QueueModel> _httpClientConnection;

        public QueueService([FromServices] IHttpConnectionFactory<QueueModel> httpClientConnection)
        {
            _httpClientConnection = httpClientConnection;
        }

        public async Task<QueueModel> Get(string queueID)
        {
            var result = await _httpClientConnection.GetAsync(int.Parse(queueID));
            return result;
        }

        public async Task<List<QueueModel>> GetAll()
        {
            var result = await _httpClientConnection.GetAllAsync();
            return (List<QueueModel>)result;
        }
    }
}

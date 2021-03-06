﻿using CallBack_Model.Model;
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
        private IHttpConnectionFactory<QueueModel> _httpClient;

        public QueueService([FromServices] IHttpConnectionFactory<QueueModel> httpClientConnection)
        {
            _httpClient = httpClientConnection;
        }

        public async Task<QueueModel> Get(string queueID)
        {
            var result = await _httpClient.GetAsync(int.Parse(queueID));
            return result;
        }

        public async Task<List<QueueModel>> GetAll()
        {
            var result = await _httpClient.GetAllAsync();
            return (List<QueueModel>)result;
        }
    }
}

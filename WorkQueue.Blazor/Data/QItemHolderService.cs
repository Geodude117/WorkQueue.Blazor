using CallBack_Model.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkQueue.Blazor.Helpers;

namespace WorkQueue.Blazor.Data
{
    public class QItemHolderService
    {
        private readonly IHttpConnectionFactory<QItemHolder> _httpClientConnection;

        public QItemHolderService([FromServices] HttpConnectionFactory<QItemHolder> httpClientConnection)
        {
            _httpClientConnection = httpClientConnection;
        }
        public async Task<bool> PostQItem(QItemHolder model)
        {
            var result = await _httpClientConnection.PostAsync(model);
            return result;
        }

        public async Task<bool> PostCompleteQItem(QItemHolder model)
        {
            var result = await _httpClientConnection.PutAsync(model);
            return result;
        }
    }
}

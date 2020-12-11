using CallBack_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkQueue.Blazor.Helpers;

namespace WorkQueue.Blazor.Data
{
    public class QItemHolderService
    {
        private IHttpConnectionFactory<QItemHolder> _httpClient;

        public async Task<bool> PostQItem(QItemHolder model)
        {
            bool result = false;

            result = await _httpClient.PostAsync(model);

            return result;
        }

        public async Task<bool> PostCompleteQItem(QItemHolder model)
        {
            bool result = false;

            result = await _httpClient.PutAsync(model);


            return result;
        }
    }
}

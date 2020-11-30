using CallBack_Model.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using WorkQueue.Blazor.Helpers;

namespace WorkQueue.Blazor.Data
{
    public class QueueGroupService
    {
        private IHttpConnectionFactory<QueueGroup> _httpClient;

        public QueueGroupService([FromServices] IHttpConnectionFactory<QueueGroup> httpClientConnection)
        {
            _httpClient = httpClientConnection;
        }

        public async Task<List<QueueGroup>> GetAll()
        {
            var result = await _httpClient.GetAllAsync();
            return result.ToList();
        }

        public async Task<QueueGroup> Get(string groupID)
        {
            var result = await _httpClient.GetSearchAsync(new SearchParameters() { QueueGroup = int.Parse(groupID) });
            return result.FirstOrDefault();
        }
    }
}

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
        private readonly IHttpConnectionFactory<QueueGroup> _httpClientConnection;

        public QueueGroupService([FromServices] IHttpConnectionFactory<QueueGroup> httpClientConnection)
        {
            _httpClientConnection = httpClientConnection;
        }

        public async Task<List<QueueGroup>> GetAll()
        {
            var result = await _httpClientConnection.GetAllAsync();
            return result.ToList();
        }

        public async Task<QueueGroup> Get(string groupID)
        {
            var result = await _httpClientConnection.GetSearchAsync(new SearchParameters() { QueueGroup = int.Parse(groupID) });
            return result.FirstOrDefault();
        }
    }
}

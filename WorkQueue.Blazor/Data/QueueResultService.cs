using CallBack_Model.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WorkQueue.Blazor.Helpers;

namespace WorkQueue.Blazor.Data
{
    public class QueueResultService
    {
        private IHttpConnectionFactory<QResult> _httpClient;

        public QueueResultService([FromServices] IHttpConnectionFactory<QResult> httpClientConnection)
        {
            _httpClient = httpClientConnection;
        }

        public async Task<QResult[]> GetAll(string QueueId)
        {
            var result = await _httpClient.GetAllAsync(int.Parse(QueueId));
            return result.ToArray();
        }
    }
}

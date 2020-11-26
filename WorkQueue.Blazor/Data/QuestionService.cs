using DomainData.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WorkQueue.Blazor.Data
{
    public class QuestionService
    {
        public async Task<DomainViewModel> GetQuestionSet(string Id)
        {
            DomainViewModel result = new DomainViewModel();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync("http://localhost:52388/api/Question/" + Id);

                result = JsonConvert.DeserializeObject<DomainViewModel>(response);
            }

            return result;
        }
    }
}

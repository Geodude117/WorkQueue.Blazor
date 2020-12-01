using DomainData.Models.QuestionModels;
using DomainData.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WorkQueue.Blazor.Data
{
    public class QuestionService
    {
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

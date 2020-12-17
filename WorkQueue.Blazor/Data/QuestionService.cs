using DomainData.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WorkQueue.Blazor.Helpers;

namespace WorkQueue.Blazor.Data
{
    public class QuestionService
    {
        private readonly IHttpConnectionFactory<DomainViewModel> _httpClientConnection;

        public QuestionService([FromServices] IHttpConnectionFactory<DomainViewModel> httpClientConnection)
        {
            _httpClientConnection = httpClientConnection;
        }
        public async Task<IDomainViewModel> GetQuestionSet(string Id)
        {
            var result = await _httpClientConnection.GetAsync(int.Parse(Id));
            return result;
        }
    }
}

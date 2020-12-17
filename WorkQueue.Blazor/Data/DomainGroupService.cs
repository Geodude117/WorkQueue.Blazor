using CallBack_Model.Model;
using DomainData.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkQueue.Blazor.Helpers;

namespace WorkQueue.Blazor.Data
{
    public class DomainGroupService
    {
        private readonly IHttpConnectionFactory<DomainGroup> _httpClientConnection;

        public DomainGroupService([FromServices] IHttpConnectionFactory<DomainGroup> httpClientConnection)
        {
            _httpClientConnection = httpClientConnection;
        }

        public async Task<List<DomainGroup>> GetAll()
        {
            var result = await _httpClientConnection.GetAllAsync();
            return result.ToList();
        }

        public async Task<DomainGroup> Get(string domainGroupId)
        {
            var result = await _httpClientConnection.GetAsync(int.Parse(domainGroupId));
            return result;
        }
    }
}

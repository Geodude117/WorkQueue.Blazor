using DomainData.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace DomainData.Blazor.Data
{
    public class DomainTypeService
    {
        public int Post(DomainType domainType)
        {
            var json = JsonConvert.SerializeObject(domainType);

            var client = new RestClient("http://localhost:52388");
            client.AddDefaultHeader("Content-type", "application/json");
            var request = new RestRequest("api/domaintype", Method.POST);

            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(json);

            var response = client.Execute(request).Content;
            return int.Parse(response);
        }

        public List<DomainType> GetAll()
        {
            var client = new RestClient("http://localhost:52388");
            client.AddDefaultHeader("Content-type", "application/json");
            var request = new RestRequest("api/domaintype", Method.GET);

            var response = client.Execute(request).Content;

            var data = JsonConvert.DeserializeObject<List<DomainType>>(response);

            return data;
        }
    }
}

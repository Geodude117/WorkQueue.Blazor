using DomainData.Models;
using Newtonsoft.Json;
using RestSharp;

namespace DomainData.Blazor.Data
{
    public class DomainDataService
    {
        public int Post(DomainGroup domainGroup)
        {
            var json = JsonConvert.SerializeObject(domainGroup);

            var client = new RestClient("http://localhost:52388");
            client.AddDefaultHeader("Content-type", "application/json");
            var request = new RestRequest("api/domaindata", Method.POST);

            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(json);

            var response = client.Execute(request).Content;
            return int.Parse(response);
        }
    }
}

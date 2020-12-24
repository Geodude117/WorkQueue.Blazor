using DomainData.Models;
using Newtonsoft.Json;
using RestSharp;
namespace DomainData.Blazor.Data
{
    public class DomainInformationService
    {
        public int Post(DomainInformation domainInformation)
        {
            var json = JsonConvert.SerializeObject(domainInformation);

            var client = new RestClient("http://localhost:52388");
            client.AddDefaultHeader("Content-type", "application/json");
            var request = new RestRequest("api/domaininformation", Method.POST);

            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(json);

            var response = client.Execute(request).Content;
            return int.Parse(response);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainData.BusinessLogic.DomainInformation;
using DomainData.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DomainData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomainInformationController : ControllerBase
    {
       
        // GET api/<DomainInformationController>/5
        [HttpGet("{groupId}")]
        public async Task<IEnumerable<DomainInformation>> Get(int groupId, [FromServices] IDomainInformationBusiness InformationBusiness)
        {
            return await InformationBusiness.GetDomainInformationByGroupId(groupId);
        }

        // POST api/<DomainInformationController>
        [HttpPost]
        public async Task<int?> Post([FromBody] DomainInformation domainInformationModel, [FromServices] IDomainInformationBusiness InformationBusiness)
        {
            return await InformationBusiness.PostDomainInformation(domainInformationModel);
        }

    }
}

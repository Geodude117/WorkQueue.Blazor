using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainData.BusinessLogic.DomainGroup;
using DomainData.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DomainData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomainDataController : ControllerBase
    {
        // GET: api/<DomainDataController>
        [HttpGet]
        public async Task<IEnumerable<DomainGroup>> Get([FromServices] IDomainGroupBusiness GroupBusiness)
        {
            return await GroupBusiness.GetAllDomainGroup();
        }

        // GET api/<DomainDataController>/5s
        [HttpGet("{id}")]
        public async Task<DomainGroup> Get(int id, [FromServices] IDomainGroupBusiness GroupBusiness)
        {
            return await GroupBusiness.GetDomainGroup(id);
        }

        // POST api/<DomainDataController>
        [HttpPost]
        public async Task<int?> Post([FromBody] DomainGroup domainGroupModel, [FromServices] IDomainGroupBusiness GroupBusiness)
        {
            return await GroupBusiness.PostDomainGroup(domainGroupModel);
        }

    }
}

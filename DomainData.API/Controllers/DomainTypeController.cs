using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainData.BusinessLogic.DomainType;
using DomainData.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DomainData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DomainTypeController : ControllerBase
    {
        // GET: api/<DomainType>
        [HttpGet]
        public async Task<IEnumerable<DomainType>> Get([FromServices] IDomainTypeBusiness TypeBusiness)
        {
            return await TypeBusiness.GetAllDomainType();
        }

        // GET api/<DomainType>/5
        [HttpGet("{id}")]
        public async Task<DomainType> Get(int id, [FromServices] IDomainTypeBusiness TypeBusiness)
        {
            return await TypeBusiness.GetDomainType(id);
        }

        // POST api/<DomainType>
        [HttpPost]
        public async Task<int?> Post([FromBody] DomainType domainTypeModel, [FromServices] IDomainTypeBusiness TypeBusiness)
        {
            return await TypeBusiness.PostDomainType(domainTypeModel);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallBack_Model.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_QBusiness.QueueResult;

namespace WebAPI_Queue.Controllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]"), ApiVersion("1.0")]
    public class QueueResultController : Controller
    {
        // GET: api/QueueResult/5
        [HttpGet("{ID}", Name = "Get")]
        public async Task<ActionResult> Get([FromRoute]int ID, [FromServices] IQResultBusiness QBusiness)
        {
            return Ok(await QBusiness.GetQResult(ID));
        }
    }
}
 
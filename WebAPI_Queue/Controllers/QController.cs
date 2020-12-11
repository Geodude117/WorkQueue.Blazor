using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI_QBusiness.Indivdual_Queues;

namespace WebAPI_Queue.Controllers
{


    /// <summary>
    /// This gets actual queue information about the queue in the database.
    /// </summary>
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]"), ApiVersion("1.0")]
 
    public class QController : Controller
    {
        [HttpGet, Route("{QueueID}")]
        public async Task<ActionResult> Get([FromRoute]int QueueID, [FromServices] IQBusiness Queue)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest);
            return Ok(await Queue.GetQAsync(QueueID));
        }


        [HttpGet]
        public async Task<ActionResult> GetAsync([FromServices] IQBusiness Queue)
        {
            return Ok(await Queue.GetQCollectionAsync());
        }
    }
}

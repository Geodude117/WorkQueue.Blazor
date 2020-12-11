using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI_QBusiness.QueueGroup_Logic;

namespace WebAPI_Queue.Controllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]"), ApiVersion("1.0")]
    public class QueueGroupController : Controller
    {
        // GET: api/QueueGroup
        [HttpGet]
        public async Task<ActionResult> GetAsync([FromServices] IQueueGroupBusiness Qgroup)
        {
            return Ok(await Qgroup.GetQGroup());
        }  
    }
}

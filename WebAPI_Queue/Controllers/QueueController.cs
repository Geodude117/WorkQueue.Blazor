using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallBack_Model.Interface;
using CallBack_Model.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI_QBusiness;

namespace WebAPI_Queue.Controllers
{   
    /// <summary>
    /// This is a rather badly named controller which really gets you information about Queue Items.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]"), ApiVersion("1.0")]
    public class QueueController : Controller
    {
       
        /// <summary>
        /// GET api/Queue/5
        /// </summary>
        /// <param name="QueueGroupID"> Refers to database Queue Group Gives active gorupset.</param>
        /// <param name="QBusiness">Injected via StartupCs</param>
        /// <param name="logger"> Uses nLog</param>
        /// <returns></returns>
        [HttpGet, Route("{QueueGroupID}")]
        public async Task<ActionResult> Get([FromRoute]int QueueGroupID, [FromServices] IQueueBusiness QBusiness)
        { 
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest);
            return Ok(await QBusiness.Get_QueueItmsAsync(QueueGroupID));
        }

        // GET api/Queue/Search     
        [HttpPut,Route("Search")]
        public async Task<ActionResult> Get([FromBody]SearchParameters searchparam, [FromServices] IQueueBusiness QBusiness, [FromServices]ILogger<QueueController> logger)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError("Invalid Model SearchParameters: QueueController, Get Action supplied invalid model.");
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            return Ok(await QBusiness.Get_QueueItms(searchparam));
        }

        // PUT api/Queue
        /// <summary>
        /// Put is a put Edit a Put completed can only be called from the connecting Qitem Model.
        /// </summary>
        /// <param name="Summary"></param>
        /// <param name="QBusiness"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> Put([FromBody]QueueItem QItem, [FromServices] IQueueBusiness QBusiness, [FromServices]ILogger<QueueController> logger)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError("Invalid Model SearchParameters: QueueController, Get Action supplied invalid model.");
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            return await QBusiness.Put_QueueItemEditAsync(QItem)? Ok() : StatusCode(StatusCodes.Status400BadRequest);
        }

    }
}

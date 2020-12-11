using System;
using System.Threading.Tasks;
using CallBack_Model.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebAPI_QBusiness;

namespace WebAPI_Queue.Controllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]"), ApiVersion("1.0")]
    public class CSUController : Controller
    {

        // GET: api/CSU/5
        [HttpGet("{ID}" )]
        public async Task<ActionResult> Get([FromRoute]int ID, [FromServices] ICSUBusiness csuBusiness)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest);
            return Ok(await csuBusiness.GetCSUItem(ID));
        }
        
        // POST: api/CSU
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody]QItemHolder postCSu,[FromServices] ICSUBusiness csuBusiness,[FromServices]ILogger<CSUController> logger)
        {
            CSU_Callback itmCallback;
            try
            {
                itmCallback = JsonConvert.DeserializeObject<CSU_Callback>(postCSu.TModel.ToString());
            }
            catch (JsonException ex)
            {
                logger.LogError("Invalid Model: CSU_Callback Model required in CSUController, PostAsync.");
                throw new JsonException("CSU Callback Item expected.",ex);
            }

            if (!ModelState.IsValid)
            {
                logger.LogError("Invalid Model State: CSU_Callback Model is in invalid state, in CSUController, PostAsync.");
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            if (await csuBusiness.Post(postCSu.queueItem, itmCallback))
                return Ok();
            logger.LogCritical("CSUController failed to PostAsync ActionResult: Post attempted failed.");
            return StatusCode(StatusCodes.Status500InternalServerError); 
        }
        
        // PUT: api/CSU
        [HttpPut]
        public async Task<ActionResult> PutEdit([FromBody]CSU_Callback CSuItem, [FromServices] ICSUBusiness csuBusiness, [FromServices]ILogger<CSUController> logger)
        {
            //if (!ModelState.IsValid)
            //{
            //    logger.LogError("Invalid Model State: CSU_Callback Model is in invalid state, in CSUController, PutEdit.");
            //    return StatusCode(StatusCodes.Status400BadRequest);
            //}

            if (await csuBusiness.PutCSUItemEdit(CSuItem))
                return Ok();
            logger.LogCritical("CSUController failed to PutEdit ActionResult: Put edit attempted failed.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // PUT: api/CSU/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutComplete([FromRoute]int id, [FromBody]QItemHolder postCSu, 
            [FromServices] ICSUBusiness csuBusiness,[FromServices]IConfiguration config,[FromServices]ILogger<CSUController> logger)
        {
            CSU_Callback itmCallback;
            try
            {
                itmCallback = JsonConvert.DeserializeObject<CSU_Callback>(postCSu.TModel.ToString());
            }
            catch (JsonException ex)
            {
                logger.LogError("Invalid Model: CSU_Callback Model required in CSUController, PostAsync.");
                throw new JsonException("CSU Callback Item expected.", ex);
            }

            if (!ModelState.IsValid)
            {
                logger.LogError("Invalid Model State: CSU_Callback Model is in invalid state, in CSUController, PutComplete.");
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            if (await csuBusiness.PutItemCompleted(id, postCSu.queueItem, itmCallback, config.GetConnectionString("NoteController")))
                return Ok();
            logger.LogCritical("CSUController failed to PutComplete ActionResult: Put Complete attempted failed.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

    }
}

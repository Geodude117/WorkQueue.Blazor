using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CallBack_Model.Model;
using DomainData.Models.DPABreachModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebAPI_QBusiness.DPA_Breach_Logic;

namespace WebAPI_Queue.Controllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]"), ApiVersion("1.0")]
    public class DPABreachStage2Controller : Controller
    {
        // GET: api/DPA/3
        [HttpGet("{ID}")]
        public async Task<ActionResult> Get([FromRoute]int id, [FromServices] IBreachStage2Business breach2Buiness)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest);
            return Ok(await breach2Buiness.GetBreachStage2Async(id));
        }

        // POST: api/DPABreach
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody]QItemHolder postDpaBreachStage2, [FromServices] IBreachStage2Business breachStage2Business, [FromServices]ILogger<DPABreachStage2Controller> logger)
        {
            BreachStage2 itmBreachStage2;
            try
            {
                itmBreachStage2 = JsonConvert.DeserializeObject<BreachStage2>(postDpaBreachStage2.TModel.ToString());

            }
            catch (JsonException ex)
            {
                logger.LogError("Invalid Model: BreachStage2 Model required in DPABreachStage2Controller, PostAsync.");
                throw new JsonException("Breach Stage 2 Item expected.", ex);
            }

            //if (!ModelState.IsValid)
            //{
            //    logger.LogError("Invalid Model State: BreachStage2 Model is in invalid state, in DPABreachStage2Controller, PostAsync.");
            //    return StatusCode(StatusCodes.Status400BadRequest);
            //}

            if (await breachStage2Business.PostAsync(postDpaBreachStage2.queueItem, itmBreachStage2))
                return Ok();
            logger.LogCritical("CSUController failed to PostAsync ActionResult: Post attempted failed.");
            return StatusCode(StatusCodes.Status500InternalServerError);

        }

        // PUT: api/DPABreach
        [HttpPut]
        public async Task<ActionResult> PutEdit([FromBody]BreachStage2 dpaBreachItem, [FromServices] IBreachStage2Business breachStage2Business, [FromServices]ILogger<DPABreachStage2Controller> logger)
        {

            if (await breachStage2Business.PutBreachStage2EditAsync(dpaBreachItem))
                return Ok();
            logger.LogCritical("DPABreachStage2Controller failed to PutEdit ActionResult: Put edit attempted failed.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // PUT: api/DPABreach/3
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCompleteAsync([FromRoute]int id, [FromBody]QItemHolder postDpaBreachStage2, [FromServices] IBreachStage2Business breachStage2Business, [FromServices]ILogger<DPABreachStage2Controller> logger)
        {
            BreachStage2 itmBreachStage2;
            try
            {
                itmBreachStage2 = JsonConvert.DeserializeObject<BreachStage2>(postDpaBreachStage2.TModel.ToString());
            }
            catch (JsonException ex)
            {
                logger.LogError("Invalid Model: BreachStage2 Model required in DPABreachStage2Controller, PostAsync.");
                throw new JsonException("Breach Stage 2 Item expected.", ex);
            }

            if (!ModelState.IsValid)
            {
                logger.LogError("Invalid Model State: BreachStage2 Model is in invalid state, in DPABreachStage2Controller, PostAsync.");
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            if (await breachStage2Business.PutBreachStage2EditCompleteAsync(id, postDpaBreachStage2.queueItem, itmBreachStage2))
                return Ok();
            logger.LogCritical("CSUController failed to PutComplete ActionResult: Put Complete attempted failed.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }


    }

}

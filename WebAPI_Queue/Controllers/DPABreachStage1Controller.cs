using System.Threading.Tasks;
using CallBack_Model.Model;
using DomainData.Models.DPABreachModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebAPI_QBusiness.DPA_Breach_Logic;

namespace WebAPI_Queue.Controllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]"), ApiVersion("1.0")]
    public class DPABreachStage1Controller : Controller
    {
        // GET: api/DPA/3
        [HttpGet("{ID}")]
        public async Task<ActionResult> Get([FromRoute]int id, [FromServices] IBreachStage1Business breach1Buiness)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest);
            return Ok( await breach1Buiness.GetBreachStage1Async(id));
        }

        // POST: api/DPABreach
        [HttpPost] 
        public async Task<ActionResult> PostAsync([FromBody]QItemHolder postDpaBreachStage1,[FromServices] IBreachStage1Business breachStage1Business, [FromServices]ILogger<DPABreachStage1Controller> logger)
        {
            BreachStage1 itmBreachStage1;
            try
            {
                itmBreachStage1 = JsonConvert.DeserializeObject<BreachStage1>(postDpaBreachStage1.TModel.ToString());

            }catch(JsonException ex)
            {
                logger.LogError("Invalid Model: BreachStage1 Model required in DPABreachStage1Controller, PostAsync.");
                throw new JsonException("Breach Stage 1 Item expected.", ex);
            }

            if (!ModelState.IsValid)
            {
                logger.LogError("Invalid Model State: BreachStage1 Model is in invalid state, in DPABreachStage1Controller, PostAsync.");
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            if (await breachStage1Business.PostAsync(postDpaBreachStage1.queueItem, itmBreachStage1))
                return Ok();
            logger.LogCritical("CSUController failed to PostAsync ActionResult: Post attempted failed.");
            return StatusCode(StatusCodes.Status500InternalServerError);

        }
        
        // PUT: api/DPABreach
        [HttpPut]
        public async Task<ActionResult> PutEdit([FromBody]BreachStage1 dpaBreachItem, [FromServices] IBreachStage1Business breachStage1Business, [FromServices]ILogger<DPABreachStage1Controller> logger)
        {

            if (await breachStage1Business.PutBreachStage1EditAsync(dpaBreachItem))
                return Ok();
            logger.LogCritical("DPABreachStage1Controller failed to PutEdit ActionResult: Put edit attempted failed.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        // PUT: api/DPABreach/3
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCompleteAsync([FromRoute]int id, [FromBody]QItemHolder postDpaBreachStage1, [FromServices] IBreachStage1Business breachStage1Business, [FromServices]ILogger<DPABreachStage1Controller> logger)
        {
            BreachStage1 itmBreachStage1;
            try
            {
                itmBreachStage1 = JsonConvert.DeserializeObject<BreachStage1>(postDpaBreachStage1.TModel.ToString());
            }
            catch (JsonException ex)
            {
                logger.LogError("Invalid Model: BreachStage1 Model required in DPABreachStage1Controller, PostAsync.");
                throw new JsonException("Breach Stage 1 Item expected.", ex);
            }

            if (!ModelState.IsValid)
            {
                logger.LogError("Invalid Model State: BreachStage1 Model is in invalid state, in DPABreachStage1Controller, PostAsync.");
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            if (await breachStage1Business.PutBreachStage1EditCompleteAsync(id, postDpaBreachStage1.queueItem, itmBreachStage1))
                return Ok();
            logger.LogCritical("CSUController failed to PutComplete ActionResult: Put Complete attempted failed.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }


    }
}
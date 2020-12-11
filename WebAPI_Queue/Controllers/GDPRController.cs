using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gdpr_Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_QBusiness.GDPR_Logic;

namespace WebAPI_Queue.Controllers
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]"), ApiVersion("1.0")]
    public class GDPRController : Controller
    {
        private readonly IGdprInterface _gdprInterface;

        public GDPRController([FromServices] IGdprInterface gdprInterface)
        {
            _gdprInterface = gdprInterface;
        }

        [HttpPut]
        public async Task<ActionResult> AnonymiseMeAsync([FromBody]GdprSearchModel searchModel)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest);
            return Ok(await _gdprInterface.AnonymiseMe(searchModel));
        }

        [HttpDelete]
        public async Task<ActionResult> ForgetMe([FromBody]GdprSearchModel searchModel)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest);
            return Ok(await _gdprInterface.ForgetMe(searchModel));
        }
    }
}

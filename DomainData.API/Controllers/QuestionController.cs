using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainData.BusinessLogic.QuestionViewModel;
using DomainData.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DomainData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
       
        // GET api/<QuestionController>/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id, [FromServices] IQuestionBusiness QuestionBusiness )
        {
            var result =  await QuestionBusiness.GetQuestionSet(id);

            string serializedJson = JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
            });

            return serializedJson;
        }

    }
}

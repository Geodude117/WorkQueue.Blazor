using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainData.BusinessLogic.QuestionViewModel;
using DomainData.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DomainData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
       
        // GET api/<QuestionController>/5
        [HttpGet("{id}")]
        public async Task<DomainViewModel> Get(int id, [FromServices] IQuestionBusiness QuestionBusiness )
        {
            var result =  await QuestionBusiness.GetQuestionSet(id);
            return result;
        }

    }
}

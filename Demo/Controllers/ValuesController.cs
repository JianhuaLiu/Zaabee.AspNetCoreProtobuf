using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpPost]
        public IEnumerable<TestDto> Post([FromBody]IEnumerable<TestDto> dtos)
        {
            return dtos;
        }
    }
}
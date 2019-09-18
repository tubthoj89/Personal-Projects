using Calculator.Interface;
using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Calculator.Factory;

namespace Calculator.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HistoryController : ApiController
    {
        ICalculationRepository _repo = CalculatorFactory.GetRepository();

        [HttpGet]
        [Route("api/history/calculations")]
        //[AcceptVerbs("GET")]
        public IHttpActionResult History()
        {
            IEnumerable<Calculation> found = _repo.GettAllCalculations();
            if (found == null)
            {
                return NotFound();
            }
            return Ok(found);
        }
    }
}

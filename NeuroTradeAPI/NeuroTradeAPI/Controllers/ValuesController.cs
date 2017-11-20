using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NeuroTradeAPI.Controllers
{
    [Route("api/v0/[controller]")]
    public class QuotesController : Controller
    {
        // GET /quotes
        [HttpGet]
        public IEnumerable<string> Get()    //for humans
        {
            return new string[] {"SPFB.RTS-12.17 | 2017.11.01 | 10:30:00 | 1234 | 1243 | 1212 | 1221",
                                 "SPFB.RTS-12.17 | 2017.11.01 | 11:00:00 | 1243 | 1234 | 1200 | 1233"};
        }

        // GET api/quotes/5
        [HttpGet("{id1-id2}")]
        public string Get(int id1, int id2)
        {
            return string.Format("{0} candles", id2 - id1 + 1);
        }

        // POST api/quotes
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/quotes/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/quotes/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
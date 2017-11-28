using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NeuroTradeAPI.Controllers
{
    [Route("api/v0/[controller]")]
    public class QuotesController : Controller
    {
        private readonly ApplicationContext _context;

        public QuotesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET /api/v0/quotes
        [HttpGet]
        public ActionResult Get()
        {
            var batches = _context.Batches;
            return Json(from batch in batches select new Dictionary<string, object>()
            {
                {"id", batch.BatchId},
                {"Ticker", batch.Alias},
                {"Start", batch.Timestamp.ToString()},
                {"Interval", batch.Interval}
            });
        }

        // GET api/v0/quotes/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var b = _context.Batches.Find(id);
            if (b == null)
                return NotFound("Batch not found");
            
            var batch = _context.Candles.Where(c => c.Batch == b);
            return Json(new Dictionary<string, object>()
            {
                {"Ticker", b.Alias},
                {"Start", b.Timestamp.ToString()},
                {"Interval", b.Interval},
                {
                    "Candles", 
                    from c in batch select (new Dictionary<string, object>()
                    {
                        {"id",c.CandleId},{"open",c.Open},{"close",c.Close},{"low",c.Low},{"high",c.High},{"volume",c.Volume}
                    })
                }
            });
        }

        // POST api/v0/quotes
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/v0/quotes/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/v0/quotes/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
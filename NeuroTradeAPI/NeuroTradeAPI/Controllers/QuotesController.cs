using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDownloader;
using Microsoft.AspNetCore.Mvc;


namespace NeuroTradeAPI.Controllers
{
    [Route("api/v0/[controller]")]
    public class QuotesController : Controller
    {
        private ApplicationContext _context;

        public QuotesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET /api/v0/quotes
        [HttpGet]
        public ActionResult Get()
        {

            var intrums = _context.Instruments;
            return Json(from inst in intrums select new Dictionary<string, object>()
            {
                {"Instument", inst.toDict()},
                {"Batches", _context.Batches.Where(b=>b.Instrument==inst).ToList()}
            });
        }

        // GET api/v0/quotes/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
//            var b = _context.Batches.Find(id);
//            if (b == null)
//                return NotFound("Batch not found");
//            
//            var batch = _context.Candles.Where(c => c.Batch == b);
//            return Json(new Dictionary<string, object>()
//            {
//                {"Batch", b.toDict()},
//                {
//                    "Candles", 
//                    from c in batch select c.toDict()
//                }
//            });
            return Ok();
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
    [Route("api/v0/[controller]")]
    public class JobsController : Controller
    {
        private static string msg_pipe = "";
        
        // GET api/v0/jobs
        [HttpGet]
        public string Get()
        {
            if (msg_pipe.Length == 0)
            {
                Response.StatusCode = 204;
                return "";
            }

            String tmp = msg_pipe;
            msg_pipe = "";
            return tmp;
        }
        
        // POST api/v0/jobs
        [HttpPost]
        public string Post(string path)
        {
            Downloader.Reqest(path, GetResponse);
            return "ok, we'll check it out soon\n"+path;
        }

        private static void GetResponse(DownloadingResult response)
        {
            msg_pipe = response.success ? 
                String.Format("Task {0} succeeded.\nReceived {1} candles for {2}",
                    response.name, response.dataList.Count, response.dataList[0]._ticker) :
                String.Format("Task {0} failed.\nError: {1}", response.name, response.error);
            
            Console.WriteLine(msg_pipe);
        }
    }
}
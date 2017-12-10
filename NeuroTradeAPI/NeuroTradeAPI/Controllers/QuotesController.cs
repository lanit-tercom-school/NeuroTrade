using System;
using System.Collections.Generic;
using System.Linq;
using CoreDownloader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeuroTradeAPI.Entities;
using Npgsql;


namespace NeuroTradeAPI.Controllers
{
    [Route("api/v0/[controller]")]
    public class QuotesController : Controller
    {

        // GET /api/v0/quotes
        [HttpGet]
        public ActionResult Get()
        {
            var context = new ApplicationContext();
//            var instrums = context.Batches.GroupBy(batch => batch.Instrument.InstrumentId);
            var instrums2 = context.Instruments.Join(context.Batches, instr => instr.InstrumentId,
                batch => batch.InstrumentId, (instr, batch) => new {instr, Batch = batch}).GroupBy(arg => arg.instr.Alias);
            return Json(from inst in instrums2
                select new Dictionary<string, object>()
                {
                    {"Instument", inst.Key},
                    {"Batches", from b in inst select b.Batch.toDict()}
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

        private static async void GetResponse(DownloadingResult response)
        {
            if (!response.success)
            {
                msg_pipe += string.Format("Task {0} failed.\nError: {1}", response.name, response.error);
                Console.WriteLine(msg_pipe);
                return;
            }
            if (!response.dataList.Any())
            {
                msg_pipe += "No data there\n";
                Console.WriteLine(msg_pipe);
                return;
            }
            var context = new ApplicationContext();
            try
            {
                var firstCandle = response.dataList[0];
                msg_pipe = String.Format("Task {0} succeeded.\nReceived {1} candles for {2}\n\n",
                    response.name, response.dataList.Count, firstCandle._ticker);

                // Converting and making sure there are valid candles
                List<Candle> candles = new List<Candle>();
                foreach (Data data in response.dataList)
                {
                    Candle c = Candle.ConvertData(data, 1);
                    if (c == null)
                        msg_pipe += "Couldn't parse candle data";
                    else
                    {
                        candles.Add(c);
                        msg_pipe += string.Join("; ", c.toDict()) + '\n';
                    }
                }
                if (!candles.Any())
                {
                    msg_pipe += "\nAre you sure that data corresponds to Candle model?\n";
                    return;
                }
                TimeSpan interval;
                try
                {
                    interval = Batch.StrToInterval(firstCandle._per);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e);
                    msg_pipe += "\nCouldn't read a valid interval from the first line\n";
                    return;
                }

                // Adding Instrument and Batch
                Instrument instrument = await context.Instruments.FirstOrDefaultAsync(instr =>
                    instr.DownloadAlias == firstCandle._ticker);
                if (instrument == null)
                {
                    msg_pipe += "A new Instrument is being created\n";
                    instrument = new Instrument {Alias = firstCandle._ticker, DownloadAlias = firstCandle._ticker};
                    await context.Instruments.AddAsync(instrument);
                    await context.SaveChangesAsync();
                }
                msg_pipe += string.Format("Current instrument's id is {0}\n", instrument.InstrumentId);

                Batch batch = new Batch
                {
                    BeginTime = candles[0].BeginTime,
                    EndTime = candles.Last().BeginTime + interval,
                    InstrumentId = instrument.InstrumentId,
                    Interval = interval
                };
                await context.Batches.AddAsync(batch); // SORRY. I have no ideas about why async fails
                await context.SaveChangesAsync();
                msg_pipe += string.Format("Current batch's id is {0}\n", batch.BatchId);

                candles.ForEach(c => c.BatchId = batch.BatchId);
                await context.Candles.AddRangeAsync(candles);
                await context.SaveChangesAsync();
                msg_pipe += string.Format("That's all\n");
            }
            catch (NpgsqlException e)
            {
                msg_pipe += string.Format("Operation failed due to database error: {0}\n", e.ToString());
            }
            catch (NullReferenceException e)
            {
                msg_pipe += "No data there\n";
            }
            Console.WriteLine(msg_pipe);
        }
    }
}
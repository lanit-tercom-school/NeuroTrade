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
                batch => batch.InstrumentId, (instr, batch) => new {instr, Batch = batch})
                .OrderBy(arg => arg.Batch.BeginTime)
                .ThenBy(arg => arg.Batch.BatchId)
                .GroupBy(arg => arg.instr.Alias);
            return Json(from inst in instrums2
                select new Dictionary<string, object>()
                {
                    {"Instument", inst.Key},
                    {"Batches", from b in inst select b.Batch.toDict()}
                });
        }

        // GET api/v0/quotes/filter?param=...
        [Route("filter")]
        public ActionResult Filter(string instrument, string batch, string from, string to)
        {
            int id;
            var context = new ApplicationContext();
            if (!DateTime.TryParse(from, out var dtFrom))
                dtFrom = DateTime.MinValue;
            if (!DateTime.TryParse(to,   out var dtTo))
                dtTo = DateTime.MaxValue;
            
            if (instrument != null)
            {
                var target = int.TryParse(instrument, out id) ? context.Instruments.Find(id)
                    : context.Instruments.FirstOrDefault(instr => instr.DownloadAlias == instrument || instr.Alias == instrument);
                if (target == null)
                    return NotFound("Instrument not found");
                var candles = context.Batches
                    .Where(_batch => _batch.InstrumentId == target.InstrumentId)
                    .OrderBy(_batch => _batch.BeginTime)
                    .Join(context.Candles, batch1 => batch1.BatchId, candle => candle.BatchId,
                        (batch1, batchCandle) => new {batch1, batch_candle = batchCandle})
                    .OrderBy(arg => arg.batch_candle.BeginTime)
                    .Where(arg => arg.batch_candle.BeginTime >= dtFrom && arg.batch_candle.BeginTime < dtTo)
                    .ToList();
                
                return Json(new Dictionary<string, object>()
                {
                    {"Request", new Dictionary<string, object>()
                        {
                            {"Instrument", target.toDict()},
                            {"From", dtFrom},
                            {"To", dtTo}
                        }
                    },
                    {"Result", from cndl in candles select cndl.batch_candle.toDict()}  
                });

            }    else if (batch != null)
            {
                if (!int.TryParse(batch, out id))
                    return BadRequest("Batch must be presented by id");
                var target = context.Batches.Find(id);
                var targetInst = context.Instruments.FirstOrDefault(_instr => _instr.InstrumentId == target.InstrumentId);
                var candles = context.Candles.Where(cndl => cndl.BatchId == target.BatchId &&
                                                            cndl.BeginTime >= dtFrom && cndl.BeginTime < dtTo)
                                             .ToList();
                if (!candles.Any())
                    return NoContent();
                return Json(new Dictionary<string, object>()
                {
                    {"Request", new Dictionary<string, object>()
                        {
                            {"Instrument", targetInst.toDict()},
                            {"Batch", target.toDict()},
                            {"From", dtFrom},
                            {"To", dtTo}
                        }
                    },
                    {"Result", from cndl in candles select cndl.toDict()}  
                });
            }
            else
            {
                return BadRequest("Please provide 'instrument' or 'batch' parameter");
            }
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
                int limit_outp = 5;
                foreach (Data data in response.dataList)
                {
                    Candle c = Candle.ConvertData(data, 1);
                    if (c == null)
                        msg_pipe += "Couldn't parse candle data";
                    else
                    {
                        candles.Add(c);
                        if (limit_outp-- > 0)
                            msg_pipe += string.Join("; ", c.toDict()) + '\n';
                        if (limit_outp == 0)
                            msg_pipe += "............. \n";
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
                context.Batches.Add(batch);
                context.SaveChanges();

                //Uniqueness check | Overwrite if interval is less than existing
                var intersection = context.Batches
                    .Where(_batch => _batch.InstrumentId == batch.InstrumentId)
                    .Join(context.Candles, _batch => _batch.BatchId, candle => candle.BatchId,
                        (_batch, batchCandle) => new {_batch, _candle = batchCandle})
                    .Where(arg => arg._candle.BeginTime >= batch.BeginTime && arg._candle.BeginTime < batch.EndTime)
                    .GroupBy(arg => arg._candle.BatchId)
                    .ToList();
                context.Database.BeginTransaction();
                if (intersection.Any())
                {
                    msg_pipe += String.Format("Found {0} existing batches within downloaded data interval: {1}\n",
                        intersection.Count(), string.Join(", ", from _batch in intersection select _batch.Key));
                    int forDeletion = 0;
                    foreach (var _batch in intersection)
                    {
                        foreach (var btc_cndl in _batch)
                        {
                            if (batch.Interval >= btc_cndl._candle.Batch.Interval)
                            {
                                msg_pipe += "Downloaded data intersects existing candles and doesn't refine them\n";
                                return;
                            }
                            forDeletion++;
                            context.Candles.Remove(btc_cndl._candle);
                        }
                    }
                    msg_pipe += String.Format("Number of candles to be deleted: {0}\n", forDeletion);
                }

                msg_pipe += string.Format("Current batch's id is {0}\n", batch.BatchId);

                candles.ForEach(c => c.BatchId = batch.BatchId);
                context.Candles.AddRange(candles);
                context.Database.CommitTransaction();
                context.SaveChanges();
                msg_pipe += string.Format("That's all folks\n");
            }
            catch (NpgsqlException e)
            {
                msg_pipe += string.Format("Operation failed due to database error: {0}\n", e.ToString());
            }
            catch (NullReferenceException e)
            {
                msg_pipe += String.Format("No data exception:\n{0}\n", e.ToString());
            }
            catch (Exception e)
            {
                msg_pipe += String.Format("Time to revise the code: {0}", e.ToString());
            }
            Console.WriteLine(msg_pipe);
        }
    }
}
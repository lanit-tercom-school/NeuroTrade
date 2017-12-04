using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentScheduler;

namespace CoreDownloader
{
    public class Downloader
    {
        //private IQuoteStorage _storage { get; set; }
        private MD5 md5Hash = MD5.Create();

        volatile HashSet<string> hashUrls = new HashSet<string>();
        private object _lockScedule = new object();
        private object _lockHashes = new object();

        public delegate void GetResponseDelegate(DownloadingResult downloadingResult);

        private ScheduledJobRegistry _registry = new ScheduledJobRegistry();

        public Downloader()
        {
            JobManager.Initialize(_registry);
        }

        public void AddTask(DownloadingTask dTask, GetResponseDelegate callback)
        {
            string hash = GetMd5StringHash(dTask.url);
            if (!hashUrls.Contains(hash))
            {
                lock (_lockHashes)
                {
                    hashUrls.Add(hash);
                }
                DownloadingResult dResult = new DownloadingResult();
                dResult.name = hash;
                lock (_lockScedule) // this one may be unneccessary, but idk how this scheduler works inside
                {
                    JobManager.AddJob(() => DownloadAndPass(dTask, dResult),
                        schedule => schedule.WithName(hash).ToRunOnceIn(dTask.delay).Milliseconds());
                }
                JobManager.JobEnd += (info) =>
                {
                    if (info.Name.Equals(hash))
                    {
                        callback(dResult);
                    }
                };
            }

        }

        /* make sure that seperator is ',' */
        private void DownloadAndPass(DownloadingTask dTask, DownloadingResult dResult)
        {
            HttpClient httpClient = new HttpClient();
            using (Stream resultStream = httpClient.GetStreamAsync(dTask.url).Result)
            {
                Validator val = new Validator();
                val.Pass(resultStream, dResult);
                // _storage.Pass(val.dataList); 
                  
                /* just to check what has been passed */ 
                foreach (Data value in val.dataList)
                {
                    value.print();
                }
            }
        }
        
        

        private void PassData(DownloadingResult dResult)
        {
            /*passing data to database*/
        }

        private bool ValidateData(Stream resultStream, DownloadingTask dTask, DownloadingResult dResult)
        {
            Console.WriteLine("Validating result");
            /*some awesome validating methods, writind errors to dResult*/
            int c;
            /* writing result to a console just in testing and development purposes*/
            do
            {
                c = resultStream.ReadByte();
                Console.Write(Convert.ToChar(c));

            } while (c != -1);
            return true;
        }

        private string GetMd5StringHash(string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

    }


    public class ScheduledJobRegistry : Registry
    {
        public ScheduledJobRegistry()
        {
            Console.WriteLine("Registry initialized");
            /* Some initial jobs may be here */
        }
    }

    public class DownloadingTask
    {
        public string url;

//        public string result = String.Empty;
        public int delay;

        public DownloadingTask(string url, int delay)
        {
            this.url = url;
            if (delay < 0)
            {
                this.delay = 0;
            }
            else
            {
                this.delay = delay;
            }
        }
    }

    /* holds result and errors happened to pass to the calling context*/
    public class DownloadingResult
    {
        public string name;
        public bool success = true;
        public string error;
    }

}
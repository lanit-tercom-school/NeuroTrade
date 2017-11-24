using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
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
        HashSet<string> hashUrls = new HashSet<string>();
        private object _lockDownload = new object();
        private object _lockLists = new object();
        private object _lockScedule = new object();
        
        private ScheduledJobRegistry _registry = new ScheduledJobRegistry();
        List<Task> runningTasks = new List<Task>();

        public Downloader()
        {
            JobManager.Initialize(_registry);
        }
        
        public List<Task> AddTask(DownloadigTask dTask, bool another = false)
        {
            Task t1 = new Task(() => AddNewTask(dTask));
            t1.Start();
            lock (_lockLists)
            {
                runningTasks.Add(t1);
            }
            return runningTasks;
        }

        private void AddNewTask(DownloadigTask dTask)
        {
            bool done = false;
            string hname = GetMd5StringHash(dTask.url);
            if (!hashUrls.Contains(hname))
            {
                Console.WriteLine("Adding task to scheduler " + hname);
                lock (_lockScedule)
                {
                    hashUrls.Add(hname);
                    JobManager.AddJob(() => DownloadData(dTask), (s) => s.WithName(hname).ToRunNow());
                }
                JobManager.JobEnd += (info) =>
                {
                    if (info.Name.Equals(hname))
                    {
                        Console.WriteLine(info.Name + " task done");
                        done = true;
                    }    
                };
                while (!done)
                {
                    Thread.Sleep(300);
                }
            }
            
        }

        private void DownloadData(DownloadigTask dTask)
        {
            WebClient wb;
            string result;
            wb = new WebClient();
            lock (_lockDownload)
            {
                Console.WriteLine("Start fetching data");
                result = wb.DownloadString(dTask.url);
            }
            Console.WriteLine("Data fetched");
            ValidateData(result, dTask);
        }

        private void ValidateData(string result, DownloadigTask dTask)
        {
            Console.WriteLine("Validating result");
            /*some awesom validating methods*/
            dTask.result = result;
            Console.WriteLine(result);
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

    public class DownloadigTask
    {
        public string url;
        public string result = String.Empty;

        public DownloadigTask(string url)
        {
            this.url = url;
        }
    }
    
    
    
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FluentScheduler;

namespace CoreDownloader
{
    class Program
    {        
        public static void Main(string[] args)
        {
            Downloader dwnl = new Downloader();
            DownloadingTask downloadingTask = new DownloadingTask("http://export.finam.ru/export9.out/?market=45&em=182439&code=EURUSD000TOD&apply=0&df=1&" +
                                                                  "mf=10&yf=2017&from=01.11.2017&dt=16&mt=10&yt=2017&" +
                                                                  "to=16.11.2017&p=9&f=EURUSD000TOD_171101_171116&e=.txt&" +
                                                                  "cn=EURUSD000TOD&dtf=1&tmf=2&MSOR=1&mstimever=0&sep=1&sep2=1&datf=3&at=1", 3000);
            DownloadingTask downloadingTask2 = new DownloadingTask("http://export.finam.ru/export9.out/?market=45&em=182406&code=CNYRUB_TOM&apply=0&df=1&" +
                                                                   "mf=10&yf=2017&from=01.11.2017&dt=16&mt=10&yt=2017&" +
                                                                   "to=16.11.2017&p=9&f=CNYRUB_TOM_171101_171116&e=.txt&" +
                                                                   "cn=CNYRUB_TOM&dtf=1&tmf=2&MSOR=1&mstimever=0&sep=1&sep2=1&datf=3&at=1", 1000);
            dwnl.AddTask(downloadingTask, GetResponse);
            dwnl.AddTask(downloadingTask2, GetResponse);
            Console.Read();
        }

        public static void GetResponse(DownloadingResult downloadingResult)
        {
            if (downloadingResult.success)
            {
                Console.WriteLine("Task {0} succeeded", downloadingResult.name);
            }
            else
            {
                Console.WriteLine("Task {0} failed.\nError: {1}", downloadingResult.name, downloadingResult.error);
            }
        }

    }       
}
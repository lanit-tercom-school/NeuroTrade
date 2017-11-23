using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentScheduler;

namespace CoreDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start the scheduler
            Downloader dwnl = new Downloader();

            /*DownloadigTask downloadigTask1 = new DownloadigTask("http://export.finam.ru/export9.out/" +
                                                               "?market=45&em=182439&code=EURUSD000TOD&apply=0&df=1&" +
                                                               "mf=10&yf=2017&from=01.11.2017&dt=16&mt=10&yt=2017&" +
                                                               "to=16.11.2017&p=9&f=EURUSD000TOD_171101_171116&e=.txt&" +
                                                               "cn=EURUSD000TOD&dtf=1&tmf=2&MSOR=1&mstimever=0&sep=1&sep2=1&datf=3&at=1");*/
            DownloadigTask downloadigTask2 = new DownloadigTask("http://export.finam.ru/export9.out/" +
                                                               "?market=45&em=182406&code=CNYRUB_TOM&apply=0&df=1&" +
                                                                "mf=10&yf=2017&from=01.11.2017&dt=16&mt=10&yt=2017&" +
                                                                "to=16.11.2017&p=9&f=CNYRUB_TOM_171101_171116&e=.txt&" +
                                                                "cn=CNYRUB_TOM&dtf=1&tmf=2&MSOR=1&mstimever=0&sep=1&sep2=1&datf=3&at=1");
            DownloadigTask downloadigTask3 = new DownloadigTask("http://export.finam.ru/export9.out/" +
                                                               "?market=45&em=456495&code=BYNRUB_TOM&apply=0&df=1&" +
                                                                "mf=10&yf=2017&from=01.11.2017&dt=16&mt=10&yt=2017&" +
                                                                "to=16.11.2017&p=9&f=BYNRUB_TOM_171101_171116&e=.txt&" +
                                                                "cn=BYNRUB_TOM&dtf=1&tmf=2&MSOR=1&mstimever=0&sep=1&sep2=1&datf=3&at=1");

            List<Task> rt;
            //rt = dwnl.AddTask(downloadigTask1);
            rt = dwnl.AddTask(downloadigTask2);
            rt = dwnl.AddTask(downloadigTask3);
            Task.WaitAll(rt.ToArray());
            
        }

    }
    
    
}
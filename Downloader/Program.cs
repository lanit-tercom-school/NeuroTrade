using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace Downloader
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * em - Инструмент
             * df, mf, yf, from, dt, mt, yt, to - это параметры времени.
             * p - период котировок (тики, 1 мин., 5 мин., 10 мин., 15 мин., 30 мин., 1 час, 1 день, 1 неделя, 1 месяц)
             * e - расширение получаемого файла; возможны варианты - .txt либо.csv
             * dtf - формат даты (1 - ггггммдд, 2 - ггммдд, 3 - ддммгг, 4 - дд/мм/гг, 5 - мм/дд/гг)
             * tmf - формат времени (1 - ччммсс, 2 - ччмм, 3 - чч: мм: сс, 4 - чч: мм)
             * MSOR - выдавать время (0 - начала свечи, 1 - окончания свечи)
             * mstimever - выдавать время (НЕ московское - mstimever=0; московское - mstime="on", mstimever="1")
             * sep - параметр разделитель полей (1 - запятая (,), 2 - точка (.), 3 - точка с запятой (;), 4 - табуляция ("), 5 - пробел ( ))
             * sep2 - параметр разделитель разрядов (1 - нет, 2 - точка (.), 3 - запятая (,), 4 - пробел ( ), 5 - кавычка ("))
             * at - добавлять заголовок в файл (0 - нет, 1 - да)
             */

            string DateFrom = Console.ReadLine(); //Date from format dd.mm.yyyy
            string DateTo = Console.ReadLine(); //Date to
            string str = "http://export.finam.ru/data.txt?market=1&em=175924&code=POLY&apply=0&df=20&mf=5&yf=2017&from="+DateFrom+"&dt=23&mt=5&yt=2017&to="+DateTo+"&p=8&f=POLY_170620_170623&e=.txt&cn=POLY&dtf=1&tmf=1&MSOR=1&mstime=on&mstimever=1&sep=1&sep2=1&datf=1&at=1";
            WebClient wb = new WebClient();
            wb.DownloadFile(str, "C:\\Users\\ALEX\\Desktop\\Free\\data.txt");
        }
    }
}

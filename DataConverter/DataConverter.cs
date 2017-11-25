﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Downloader
{
    class DataConverter
    {
        public List<Data> dataList { get;  } = new List<Data>();
        
        /// <summary>
        /// to convert data from file to obj Data and add to dataList;
        /// make sure that the first string of file is names of fields
        /// example: <TICKER>,<PER>,<DATE>,<TIME>,<LOW>,<CLOSE>,<VOL>
        /// </summary>
        /// <param name="filePath"> file path </param>
        /// <param name="separator"> character that separaates fieelds and values </param>
        /// <exception cref="Exception"> in case of unexpected format of the first string </exception>
        public void convert(String filePath, char separator)
        {
            FileStream file = new FileStream(filePath, FileMode.Open);
            StreamReader reader = new StreamReader(file);

            string topString = reader.ReadLine();
            
            int[] dataFields = new int[Data.NumberOfFields];
            initializeArr(dataFields, -1);
            int wordsNumber = initializeFieldsArr(topString, dataFields, separator);
            
            if (wordsNumber == 0)
                throw new Exception("Converting falied: unexpected format of topString");
            
            while (!reader.EndOfStream)
            {
                string next = reader.ReadLine();
                string[] value = next.Split(separator);
                checkAndAdd(dataList, dataFields, value, wordsNumber);
            }
            reader.Close();
        }
        
        
        /// <summary>
        ///  parsing the first string of file and identifying the fields
        /// </summary>
        /// <param name="topString"> the first string of file
        /// example: <TICKER>,<PER>,<DATE>,<TIME>,<LOW>,<CLOSE>,<VOL> </param>
        /// <param name="dataFields"> array that fills with info about fields
        /// dataFields[i] = -1 means there is no i-th field from Data class;
        /// dataFields[i] = j means i-th field from Data class is j-th in first string</param>
        /// <param name="separator"> char that separeates words </param>
        /// <returns>number of fields in the first string</returns>
        private int initializeFieldsArr(string topString, int[] dataFields, char separator)
        {
            string[] splitString = topString.Split(separator);
            int numberOfWords = 0;
            for (int i = 0; i < splitString.Length; i++)
            {
                
                int rf = defineField(splitString[i]);
                if (rf == -1)
                    return 0;

                if (dataFields[rf] != -1)
                    return 0;
                
                dataFields[rf] = i;
                numberOfWords++;
            }
            return numberOfWords;
        }

        /* DFA for parsing the first string  - in case of character-by-character reading
         * i dont know do we need it? guess no, we dont, but it s already written sooo
         * private int initializeFieldsArr(string topString, int[] dataFields)
           {
               int wordNumber = 0;
               int i = 0;
               int state = 0;
               StringBuilder word = new StringBuilder("");
   
               while (i < topString.Length)
               {
                   char c;
                   switch (state)
                   {
                       // the begin of word / after ',' 
                       case 0:
                           c = topString[i];
                           i++;
                           if (c != '<')
                               return 0;
                           state = 1;
                           break;   
                       // the word / after '<'
                       case 1:
                           c = topString[i];
                           i++;
                           if (c != '>')
                               word.Append(c);
                           else
                           {
                               int rf = recognizeField(word.ToString());
                               if (rf == -1)
                                   return 0;
                               dataFields[rf] = wordNumber;
                               word.Clear();
                               wordNumber++;
                               state = 2;
                           }
                           break;
                       // the end of word / after '>'
                       case 2:
                           c = topString[i];
                           i++;
                           if (c != ',')
                               return 0;
                           state = 0;
                           break;
                       }
               }
               return wordNumber;
           }
           */

        
        /// <summary>
        /// to define the field (for using in DFA remove '<' and '>')
        /// </summary>
        /// <param name="str"> field we need to define </param>
        /// <returns> index of field in Data class </returns>
        private int defineField(string str)
        {
            switch (str)
            {
                case "<TICKER>":
                    return 0;
                case "<PER>":
                    return 1;
                case "<DATE>":
                    return 2;
                case "<TIME>":
                    return 3;
                case "<OPEN>":
                    return 4;
                case "<HIGH>":
                    return 5;
                case "<LOW>":
                    return 6;
                case "<CLOSE>":
                    return 7;
                case "<VOL>":
                    return 8;
            }
            return -1;
        }

        
        /// <summary>
        /// to check input data, convert to Data obj, add to list
        /// </summary>
        /// <param name="dataList"> list </param>
        /// <param name="dataFields"> array that contains info about fields
        /// dataFields[i] = -1 means there is no i-th field from Data class;
        /// dataFields[i] = j means i-th field from Data class is j-th in value array </param>
        /// <param name="value"> array with input data (may be in an incorrect format) </param>
        /// <param name="wordNumber"> number of words in input data </param>
        private void checkAndAdd(List<Data> dataList, int[] dataFields, string[] value, int wordNumber)
        {
            if (check(dataFields, value, wordNumber))
            {
                Data toAdd = new Data();
                toAdd.setSomeOfFields(dataFields, value);
                dataList.Add(toAdd);
            }
        }

        /// <summary>
        /// to check string with value;
        /// need to figure out more about it (but after database response), cause i met only these values:
        /// TICKER: CNYRUB_TOM, EURUSD000TOD - any characters
        /// PER: W, 1 - any characters
        /// DATE: yyyymmdd format 
        /// TIME:  675.5000000, 0000 - numbers and dot or only numbers 
        /// OPEN, HIGH, LOW, CLOSE - numbers and dot
        /// VOL: volume - only numbers
        /// </summary>
        /// <param name="dataFields"> array that contains info about fields
        /// dataFields[i] = -1 means there is no i-th field from Data class;
        /// dataFields[i] = j means i-th field from Data class is j-th in value array </param>
        /// <param name="value"> array with input data (may be in an incorrect format </param>
        /// <param name="wordNumber"> number of words in top string </param>
        /// <returns> true or false depens on result </returns>
        private bool check(int[] dataFields, string[] value, int wordNumber)
        {
            if (value.Length != wordNumber)
                return false;

            for (int i = 0; i < dataFields.Length; i++)
            {
                if (dataFields[i] != -1)
                {
                    // for Ticker and Per
                    if (i < 2 && !containsAnyChars(value[dataFields[i]]))
                    {
                        Console.WriteLine("ticker or par");
                        return false;
                    }
                    // for Date
                    if (i == 2 && !checkDate(value[dataFields[i]]))
                    {
                        Console.WriteLine("date");                    
                        return false;
                    }
                    // for Time
                    if (i == 3 && !(containsNumbersAndDot(value[dataFields[i]]) ||
                                   containsOnlyNumbers(value[dataFields[i]])))
                    {
                        Console.WriteLine("time" + value[dataFields[i]] );
                        return false;
                    }
                    // for Open, High, Low, Close
                    if (i > 3 && i < 8 && !containsNumbersAndDot(value[dataFields[i]]))
                    {
                        Console.WriteLine("open, high, low or close");
                        return false;
                    }
                    //for Vol
                    if (i == 8 && !containsOnlyNumbers(value[dataFields[i]]))
                    {
                        Console.WriteLine("vol");
                        return false;
                    }
                }
            }
            return true;
        }


        private bool containsAnyChars(string str)
        {
            return str != "";
        }
        

        private bool checkDate(string date)
        {
            return date.Length == 8 && containsOnlyNumbers(date);
        }
        

        private bool containsOnlyNumbers(string numbers)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (!((numbers[i] >= '0') && (numbers[i] <= '9')))
                    return false;
            }
            return true;
        }
        

        private bool containsNumbersAndDot(string numAndDot)
        {
            string[] parts = numAndDot.Split('.');
            return parts.Length == 2 && containsOnlyNumbers(parts[0]) && containsOnlyNumbers(parts[1]);
        }
        

        private void initializeArr(int[] arr, int value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = value;
            }
        }   
    }
}
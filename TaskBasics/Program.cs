using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TaskBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            //Example1IsThreadPool();
            //Example2IsCustomThread();
            //Example3PassDataToTask();
            //Example4PassDataByClosures();
            //Example5DangerOfClosures();
            //Example6CSharp5ChangeOfBehavior();
            //Example7PageDownload();
            //Example8BetterPageDownload();
            Example9CatchingExceptions();

            Console.ReadKey();
        }

        private static void Example9CatchingExceptions()
        {
            Task task = Task.Factory.StartNew(() => Import(@"C:\data\2.xml"));
            try
            {
                task.Wait();
            }
            catch (AggregateException e)
            {
                //tasks always throw aggregate exception
                foreach (Exception error in e.Flatten().InnerExceptions)
                {
                    Console.WriteLine("{0} : {1}", error.GetType().Name, error.Message);
                }

                // handle will be executed for each of exceptions in aggregate
                e.Handle((ex) =>
                {
                    Console.WriteLine("{0} : {1}", ex.GetType().Name, ex.Message);
                    return true;
                });

            }
        }

        private static void Import(string fullName)
        {
            XElement doc = XElement.Load(fullName);
            // process xml document
            throw new Exception("some exception");
        }

        private static void Example8BetterPageDownload()
        {
            // this solution take second thread to start operation
            // then put it back to threadpool and after completion of operation
            // takes thread again from thread pool
            var downloader = new PageDownloader();
            Task<string> downloadTask = downloader.BetterDownloadWebPageAsync("someurl");
            while (!downloadTask.IsCompleted)
            {
                Console.Write(".");
                Thread.Sleep(250);
            }
            Console.WriteLine(downloadTask.Result);
        }

        private static void Example7PageDownload()
        {
            // this solution still use 2 threads during operation
            var downloader = new PageDownloader();
            Task<string> downloadTask = downloader.DownloadWebPageAsync("someurl");
            while (!downloadTask.IsCompleted)
            {
                Console.Write(".");
                Thread.Sleep(250);
            }
            Console.WriteLine(downloadTask.Result);
        }

        private static void Example6CSharp5ChangeOfBehavior()
        {
            // C# 4.0  - will result nine 9s
            // C# 5.0+ - will result 0-9
            foreach (var i in new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 })
            {
                Task.Factory.StartNew(() => Console.WriteLine(i));
            }
        }

        private static void Example5DangerOfClosures()
        {
            for (int i = 0; i < 10; i++)
            {
                //local variable fixes problem with closures by changing the point of capture
                var localVar = i;
                Task.Factory.StartNew(() => Console.WriteLine(localVar));
            }
            //here is the capture point
        }

        private static void Example4PassDataByClosures()
        {
            //during compilation compiler build ImportClosureExample4 class
            var importer = new DataImporter();
            string importDirectory = "directory";
            Task.Factory.StartNew(() => importer.Import(importDirectory));
        }

        private static void Example3PassDataToTask()
        {
            // other solutions is just to hold some data in DataImporter fields
            // passing by object is more efficient but closures keep code simple
            var importer = new DataImporter();
            Task.Factory.StartNew(importer.Import, "directory");
        }

        private static void Example2IsCustomThread()
        {
            Task.Factory.StartNew(WhatTypeOfThreadAmI, TaskCreationOptions.LongRunning).Wait();
        }

        private static void Example1IsThreadPool()
        {
            Task.Factory.StartNew(WhatTypeOfThreadAmI).Wait();
        }

        private static void WhatTypeOfThreadAmI()
        {
            Console.WriteLine("I'm a {0} thread",
            Thread.CurrentThread.IsThreadPoolThread ? "Thread Pool" : "Custom");
        }
    }

    public class DataImporter
    {
        public void Import(object o)
        {
            Console.WriteLine((string)o);
        }
    }
    public class ImportClosureExample4
    {
        public string importDirectory;
        public DataImporter importer;

        public void ClosureMethod()
        {
            importer.Import(importDirectory);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSurprise
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskRaceExample1();
            //TaskRaceExample2();

            //ThreadExample1();

        }

        private static void TaskRaceExample1()
        {
            // Task t = Task.Factory.StartNew(Speak);
            Task t = new Task(Speak);
            t.Start();
        }

        private static void TaskRaceExample2()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Task t = new Task(Speak);
            t.Start();
            Console.WriteLine("Waiting for completion");
            t.Wait();
            Console.WriteLine("All Done");
        }

        private static void ThreadExample1()
        {
            Thread thread = new Thread(Speak);
            thread.IsBackground = false;
            thread.Start();
        }

        private static void Speak()
        {
            Console.WriteLine($"Hello World ({Thread.CurrentThread.ManagedThreadId})");
        }
    }
}

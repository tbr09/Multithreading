using System;
using System.Threading;

namespace AutoResetEventCore
{
    class Program
    {
        private static System.Threading.AutoResetEvent waitHandle = new System.Threading.AutoResetEvent(false);

        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "PrimaryThread";
            Console.WriteLine($"Main() thread: {Thread.CurrentThread.Name}({Thread.CurrentThread.ManagedThreadId})");

            Printer p = new Printer();

            Thread bgThread = new Thread(new ThreadStart(p.PrintNumbers));
            bgThread.Name = "SecondaryThread";
            bgThread.Start();

            //wait until PrintNumbers() will send signal via waitHandle (waitHandle.Set())
            waitHandle.WaitOne();

            Console.WriteLine($"i/o work finished!");

            Console.ReadKey();
        }


        public class Printer
        {
            public void PrintNumbers()
            {
                Console.WriteLine($"Make some i/o work (Thread: {Thread.CurrentThread.Name} - {Thread.CurrentThread.ManagedThreadId})");

                Thread.Sleep(3000);

                // send signal about finished task
                waitHandle.Set();
            }
        }
    }
}

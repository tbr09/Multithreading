using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoResetEvent
{
    class Program
    {
        private static System.Threading.AutoResetEvent waitHandle = new System.Threading.AutoResetEvent(false);
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "PrimaryThread";
            Console.WriteLine($"Main() thread: {Thread.CurrentThread.Name}({Thread.CurrentThread.ManagedThreadId})");

            Printer p = new Printer();
            Printer p2 = new Printer();
            Printer p3 = new Printer();

            Thread bgThread = new Thread(new ThreadStart(p.PrintNumbers));
            bgThread.Name = "SecondaryThread";
            bgThread.Start();

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

                waitHandle.Set();
            }
        }
    }
}

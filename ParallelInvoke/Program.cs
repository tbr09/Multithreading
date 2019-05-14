using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelInvoke
{
    class Program
    {
        static void Main(string[] args)
        {
            //Parallel.Invoke(SomeLongOperation, OtherLongOperation);
            // not sure which of this methods starts/ends first
            Parallel.Invoke(
                () =>
                {
                    SomeLongOperation();
                },
                () =>
                {
                    OtherLongOperation();
                });


            Console.ReadKey();
        }
        
        public static void SomeLongOperation()
        {
            Random rand = new Random();

            Console.WriteLine($"Long operation started (ThreadId:{Thread.CurrentThread.ManagedThreadId})");

            Thread.Sleep(rand.Next(1, 4) * 1000);

            Console.WriteLine("Long operation finished");
        }

        public static void OtherLongOperation()
        {
            Random rand = new Random();

            Console.WriteLine($"Other long operation started (ThreadId:{Thread.CurrentThread.ManagedThreadId})");

            Thread.Sleep(rand.Next(1, 4) * 1000);

            Console.WriteLine("Other long operation finished");
        }
    }
}

using System;
using System.Threading;

namespace MultiThreads
{
    class Program
    {
        static void Main(string[] args)
        {
            //Example1();
            //Example2Lock();
            Example3Monitor();
        }

        static void Example1()
        {
            var p = new Printer();

            Thread[] threads = new Thread[10];
            for (int i = 0; i < 10; i++)
            {
                threads[i] = new Thread(new ThreadStart(p.PrintNumbers))
                {
                    Name = $"TTread #{i}"
                };
            }

            foreach (Thread t in threads)
                t.Start();
            Console.ReadLine();
        }

        static void Example2Lock()
        {   
            var p = new ThreadSafePrinterWithLock();

            Thread[] threads = new Thread[10];
            for (int i = 0; i < 10; i++)
            {
                threads[i] = new Thread(new ThreadStart(p.PrintNumbers))
                {
                    Name = $"TTread #{i}"
                };
            }

            foreach (Thread t in threads)
                t.Start();
            Console.ReadLine();
        }

        static void Example3Monitor()
        {
            var p = new ThreadSafePrinterWithMonitor();

            Thread[] threads = new Thread[10];
            for (int i = 0; i < 10; i++)
            {
                threads[i] = new Thread(new ThreadStart(p.PrintNumbers))
                {
                    Name = $"TTread #{i}"
                };
            }

            foreach (Thread t in threads)
                t.Start();
            Console.ReadLine();
        }
    }

    public class Printer
    {
        public void PrintNumbers()
        {
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} printing numbers");
            for (int i = 0; i < 10; i++)
            {
                // Put thread to sleep for a random amount of time.
                Random r = new Random();
                Thread.Sleep(1000 * r.Next(5));
                Console.Write($"{i}, ");
            }
            Console.WriteLine();
        }
    }

    public class ThreadSafePrinterWithLock
    {
        private object ThreadLock = new object();

        public void PrintNumbers()
        {
            lock (ThreadLock)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} printing numbers");
                for (int i = 0; i < 10; i++)
                {
                    // Put thread to sleep for a random amount of time.
                    Random r = new Random();
                    Thread.Sleep(100 * r.Next(5));
                    Console.Write($"{i}, ");
                }
                Console.WriteLine();
            }
        }
    }

    public class ThreadSafePrinterWithMonitor
    {
        private object ThreadLock = new object();

        public void PrintNumbers()
        {
            Monitor.Enter(ThreadLock);
            try
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} printing numbers");
                for (int i = 0; i < 10; i++)
                {
                    // Put thread to sleep for a random amount of time.
                    Random r = new Random();
                    Thread.Sleep(100 * r.Next(5));
                    Console.Write($"{i}, ");
                }

                Console.WriteLine();
            }
            finally
            {
                Monitor.Exit(ThreadLock);
            }
        }
    }
}

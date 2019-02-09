using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManualSecondThread
{
    class Program
    {
        static void Main(string[] args)
        {
            Example1();
            //Example2();
        }

        static void Example1()
        {
            Thread.CurrentThread.Name = "PrimaryThread";
            Console.WriteLine($"Main() thread: {Thread.CurrentThread.Name}({Thread.CurrentThread.ManagedThreadId})");

            Printer p = new Printer();
            Printer p2 = new Printer();
            Printer p3 = new Printer();

            Thread thread1 = new Thread(new ThreadStart(p.PrintNumbers));
            thread1.Name = "SecondaryThread";
            thread1.Start();

            Thread thread2 = new Thread(new ParameterizedThreadStart(p.PrintXNumbers));
            thread2.Name = "SecondaryThread";
            thread2.Start(5);
            p2.PrintNumbersSlowly();

            Console.ReadKey();
        }

        static void Example2()
        {
            Thread.CurrentThread.Name = "PrimaryThread";
            Console.WriteLine($"Main() thread: {Thread.CurrentThread.Name}({Thread.CurrentThread.ManagedThreadId})");

            Printer p = new Printer();

            Thread thread1 = new Thread(new ThreadStart(p.PrintNumbers));
            thread1.Name = "SecondaryThread";
            //thread1.IsBackground = true;
            // bg with (and without) readkey 
            thread1.Start();

            Console.ReadKey();
        }
    }

    public class Printer
    {
        public void PrintNumbers()
        {
            Console.WriteLine($"Printing numbers (Thread: {Thread.CurrentThread.Name} - {Thread.CurrentThread.ManagedThreadId})");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{i} ({Thread.CurrentThread.ManagedThreadId})");
                Thread.Sleep(2000);
            }
        }

        public void PrintNumbersSlowly()
        {
            Console.WriteLine($"Printing numbers (Thread: {Thread.CurrentThread.Name} - {Thread.CurrentThread.ManagedThreadId})");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{i} ({Thread.CurrentThread.ManagedThreadId})");
                Thread.Sleep(5000);
            }
        }
        public void PrintXNumbers(object x)
        {
            Console.WriteLine($"Printing numbers (Thread: {Thread.CurrentThread.Name} - {Thread.CurrentThread.ManagedThreadId})");

            for (int i = 0; i < (int)x; i++)
            {
                Console.WriteLine($"{i} ({Thread.CurrentThread.ManagedThreadId})");
                Thread.Sleep(2000);
            }
        }
    }
}

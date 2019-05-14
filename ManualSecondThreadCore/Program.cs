using System;
using System.Threading;

namespace ManualSecondThreadCore
{
    public class Program
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
            Printer p1 = new Printer();
            Printer p2 = new Printer();

            Thread thread1 = new Thread(new ThreadStart(p.PrintNumbers));
            thread1.Name = "SecondThread";
            thread1.Start();

            Thread thread2 = new Thread(new ParameterizedThreadStart(p1.PrintXNumbers));
            thread2.Name = "ThirdThread";
            thread2.Start(5);

            p2.PrintNumbersSlowly();

            Console.ReadKey();
        }

        static void Example2()
        {
            Thread.CurrentThread.Name = "PrimaryThread";
            Console.WriteLine($"Main() thread: {Thread.CurrentThread.Name}({Thread.CurrentThread.ManagedThreadId})");

            Printer p = new Printer();

            Thread thread1 = new Thread(new ParameterizedThreadStart(p.PrintXNumbers));
            thread1.Name = "SecondThread";
            //thread1.IsBackground = true;
            // run thread with/without bg to show Console.ReadKey behaviour
            // foreground thread have the ability to prevent the current application from terminating
            thread1.Start(5);

            Console.ReadKey();
        }
    }
}

using System;
using System.Threading;

namespace ManualSecondThread
{
    public class Printer
    {
        public void PrintNumbers()
        {
            Console.WriteLine($"Printing numbers (Thread: {Thread.CurrentThread.Name} - {Thread.CurrentThread.ManagedThreadId})");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{i} (Thread: {Thread.CurrentThread.ManagedThreadId})");
                Thread.Sleep(2000);
            }
        }

        public void PrintNumbersSlowly()
        {
            Console.WriteLine($"Printing numbers (Thread: {Thread.CurrentThread.Name} - {Thread.CurrentThread.ManagedThreadId})");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"{i} (Thread: {Thread.CurrentThread.ManagedThreadId})");
                Thread.Sleep(5000);
            }
        }
        public void PrintXNumbers(object x)
        {
            Console.WriteLine($"Printing numbers (Thread: {Thread.CurrentThread.Name} - {Thread.CurrentThread.ManagedThreadId})");

            for (int i = 0; i < (int)x; i++)
            {
                Console.WriteLine($"{i} (Thread: {Thread.CurrentThread.ManagedThreadId})");
                Thread.Sleep(2000);
            }
        }
    }
}

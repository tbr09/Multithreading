using System;
using System.Threading;

namespace ThreadingNamespaceCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Current AppDomain: {Thread.GetDomain().FriendlyName}");
            Console.WriteLine($"Current Context Id: {SynchronizationContext.Current}");
            Console.WriteLine($"Thread name: {Thread.CurrentThread.Name}");
            Console.WriteLine($"Is alive?: {Thread.CurrentThread.IsAlive}");
            Console.WriteLine($"State: {Thread.CurrentThread.ThreadState}");


            Console.WriteLine($"Priority: {Thread.CurrentThread.Priority}");
            // Priority.Highest not guarant to be given highest priority
            // Threads with same priority receive the same amount of time


            //ThreadPool
            ThreadPool.GetMaxThreads(out var nWorkerThreads, out var nCompletionThreads);
            Console.WriteLine($"Worker threads: {nWorkerThreads}, I/O threads: {nCompletionThreads}");
            Console.ReadKey();
        }
    }
}

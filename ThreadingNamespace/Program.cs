using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Current AppDomain: {Thread.GetDomain().FriendlyName}");
            Console.WriteLine($"Current Context Id: {Thread.CurrentContext.ContextID}");
            Console.WriteLine($"Thread name: {Thread.CurrentThread.Name}");
            Console.WriteLine($"Is alive?: {Thread.CurrentThread.IsAlive}");
            Console.WriteLine($"State: {Thread.CurrentThread.ThreadState}");


            Console.WriteLine($"Priority: {Thread.CurrentThread.Priority}");
            // Priority.Highest not guarant to be given highest priority
            // Threads with same priority receive the same amount of time
            Console.ReadKey();
        }
    }
}

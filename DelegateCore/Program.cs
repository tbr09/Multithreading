using System;
using System.Threading;

namespace DelegateCore
{
    class Program
    {
        delegate int AddictionDelegate(int a, int b);

        static void Main(string[] args)
        {
            var externalData = "some external data";

            Console.WriteLine("Hello World!");

            AddictionDelegate add = Add;

            Console.WriteLine("Executed on thread (Main()) ThreadId: " + Thread.CurrentThread.ManagedThreadId);

            IAsyncResult result;

             result = add.BeginInvoke(10, 5, new AsyncCallback(AddComplete), externalData);

            var operationResult = add.EndInvoke(result);

            Console.ReadKey();
        }

        static int Add(int a, int b)
        {
            Console.WriteLine("Executed on thread (Add) ThreadId: " + Thread.CurrentThread.ManagedThreadId);
            return a + b;
        }

        static void AddComplete(IAsyncResult ar)
        {
            Console.WriteLine($"Write {ar.AsyncState}");
            Console.WriteLine("Executed on thread (AddComplete) ThreadId: " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}

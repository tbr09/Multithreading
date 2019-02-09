using System;
using System.Threading;

namespace DelegateCore
{
    class Program
    {
        delegate int AddictionDelegate(int a, int b);

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            AddictionDelegate add = Add;

            Console.WriteLine("Executed on thread (Main()) ThreadId: " + Thread.CurrentThread.ManagedThreadId);

            IAsyncResult result;

             result = add.BeginInvoke(10, 5, new AsyncCallback(AddComplete), null);

            var operationResult = add.EndInvoke(result);

        }

        static int Add(int a, int b)
        {
            Console.WriteLine("Executed on thread (Add) ThreadId: " + Thread.CurrentThread.ManagedThreadId);
            return a + b;
        }

        static void AddComplete(IAsyncResult ar)
        {
            Console.WriteLine("Executed on thread (AddComplete) ThreadId: " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}

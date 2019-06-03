using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Delegate
{
    class Program
    {
        delegate int AddictionDelegate(int a, int b);

        static void Main(string[] args)
        {
            Example1();
        }

        private static void Example1()
        {
            var externalData = "some external data";

            Console.WriteLine("Hello World!");

            AddictionDelegate add = Add;

            Console.WriteLine("Executed on thread (Main()) ThreadId: " + Thread.CurrentThread.ManagedThreadId);

            IAsyncResult result;

            //as you see we dont manually create threads here, ThreadPool take care of this
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

        // ----------------------------------------------
        // Custom implementation of Begin and End methods
        // ----------------------------------------------

        public IAsyncResult BeginAddition(int a, int b, AsyncCallback requestCallback, object state)
        {
            int sum = a + b;
            return new CompletedAsyncResult<int>(sum);
        }

        public int EndAddition(IAsyncResult result)
        {
            {
                CompletedAsyncResult<int> finalResult = result as CompletedAsyncResult<int>;
                return finalResult.Data;
            }
        }

        public class CompletedAsyncResult<T> : IAsyncResult
        {
            public T Data { get; set; }

            public bool IsCompleted { get; set; }

            public WaitHandle AsyncWaitHandle { get; set; }

            public object AsyncState { get; set; }

            public bool CompletedSynchronously { get; set; }

            public CompletedAsyncResult(T data)
            {
                this.Data = data;
            }
        }
    }

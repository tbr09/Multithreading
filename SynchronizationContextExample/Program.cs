using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SynchronizationContextExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!");
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }

    public class SynchronizationContextExample
    {
        private void OnPerformSearch(object sender, EventArgs e)
        {
            WebRequest req = WebRequest.Create("http://www.google.com/#q=weather");
            // Must make this call on the UI thread
            uiCtx = SynchronizationContext.Current;

            AsyncCallback callback = delegate (IAsyncResult iar)
            {
                WebResponse resp = req.EndGetResponse(iar);
                ProcessResponse(resp);
            };
            req.BeginGetResponse(callback, null);
        }
        private SynchronizationContext uiCtx;
        private void ProcessResponse(WebResponse resp)
        {
            // This code is on the threadpool thread
            StreamReader reader = new StreamReader(resp.GetResponseStream());
            SendOrPostCallback callback = delegate
            {
                // this runs on the UI thread
                UpdateUI(reader.ReadToEnd());
                // must Dispose reader here as this code runs async
                reader.Dispose();
            };
            uiCtx.Post(callback, null);
        }

        private void UpdateUI(string data)
        {

        }
    }
}

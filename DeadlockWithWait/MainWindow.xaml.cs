using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DeadlockWithWait
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine($"Context: {Thread.CurrentContext.ContextID}");
            var unSafeResult = UnSafeAsyncMethod().Result;
            Console.WriteLine($"Context: {Thread.CurrentContext.ContextID}");
        }

        public async Task<string> UnSafeAsyncMethod()
        {
            Console.WriteLine($"Context: {Thread.CurrentContext.ContextID}");
            var result = await FetchDataAsync();
            return result;
        }

        public async Task<string> SafeAsyncMethod()
        {
            Console.WriteLine($"Context: {Thread.CurrentContext.ContextID}");
            var result = await FetchDataAsync().ConfigureAwait(false);
            return result;
        }


        public async Task<string> FetchDataAsync()
        {
            await Task.Delay(100);
            return "Some data";
        }
    }
}

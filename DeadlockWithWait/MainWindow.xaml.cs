using System;
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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //MakeSomethingAsync().Wait();

            var taskWithData = MakeSomethingWithDataAsync().Result;
        }

        private async Task<string> MakeSomethingWithDataAsync()
        {
            await Task.Delay(2000).ConfigureAwait(false);
            return "Some data returned after async operation";
        }

        private async Task MakeSomethingAsync()
        {
            await Task.Delay(2000).ConfigureAwait(false);
            Console.WriteLine("Some data returned after async operation");
        }
    }
}

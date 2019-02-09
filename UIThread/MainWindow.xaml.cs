using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace UIThread
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            // This will be updated shortly
        }
        private void cmdProcess_Click(object sender, EventArgs e)
        {
            //ProcessFiles();
            //ProcessFilesParallel();
            //ProcessFilesParallelDispatcher();
            Task.Factory.StartNew(() => ProcessFilesParallelDispatcher());
        }
        private void ProcessFiles()
        {
            string[] files = Directory.GetFiles(@".\TestPictures", "*.jpg", SearchOption.AllDirectories);
            string newDir = @".\ModifiedPictures";
            Directory.CreateDirectory(newDir);

            foreach (string currentFile in files)
            {
                string filename = Path.GetFileName(currentFile);
                using (Bitmap bitmap = new Bitmap(currentFile))
                {
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    bitmap.Save(Path.Combine(newDir, filename));
                    // Print out the ID of the thread processing the current image.
                    this.Title = $"Processing {filename} on thread {Thread.CurrentThread.ManagedThreadId}";
                }
            }
        }

        private void ProcessFilesParallel()
        {
            string[] files = Directory.GetFiles(@".\TestPictures", "*.jpg", SearchOption.AllDirectories);
            string newDir = @".\ModifiedPictures";
            Directory.CreateDirectory(newDir);

            Parallel.ForEach(files, currentFile =>
                {
                    string filename = Path.GetFileName(currentFile);
                    using (Bitmap bitmap = new Bitmap(currentFile))
                    {
                        bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        bitmap.Save(Path.Combine(newDir, filename));
                        // Cannot access UI elements on secondary thread
                        this.Title = $"Processing {filename} on thread {Thread.CurrentThread.ManagedThreadId}";
                    }
                }
            );
        }

        private void ProcessFilesParallelDispatcher()
        {
            string[] files = Directory.GetFiles(@".\TestPictures", "*.jpg", SearchOption.AllDirectories);
            string newDir = @".\ModifiedPictures";
            Directory.CreateDirectory(newDir);

            Parallel.ForEach(files, currentFile =>
                {
                    string filename = Path.GetFileName(currentFile);
                    using (Bitmap bitmap = new Bitmap(currentFile))
                    {
                        bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        bitmap.Save(Path.Combine(newDir, filename));
                        // Cannot see change of title until all images have been processes because primary UI
                        // thread is still locked (waiting for other threads to finish up their bussiness

                        // Run this method with Task.Factory.StartNew() will work
                        this.Dispatcher.Invoke((Action)delegate
                            {
                                this.Title = $"Processing {filename} on thread {Thread.CurrentThread.ManagedThreadId}";
                            }
                        );
                    }
                }
            );
        }
    }
}

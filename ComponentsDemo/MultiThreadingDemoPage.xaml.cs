using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ComponentsDemo
{
    /// <summary>
    /// Interaction logic for MultiThreadingDemoPage.xaml
    /// </summary>
    public partial class MultiThreadingDemoPage : Page, INotifyPropertyChanged
    {
 

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly Progress<double> progressReporter;
        private CancellationTokenSource sumCancellationTokenSource;
        private double _progressOfSum;

        public double ProgressOfSum
        {
            get => _progressOfSum;
            set
            {
                if (_progressOfSum == value) return;
                _progressOfSum = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProgressOfSum)));
            }
        }

        private long _resultOfSum;

        public long ResultOfSum
        {
            get { return _resultOfSum; }
            set
            {
                if (_resultOfSum == value) return;
                _resultOfSum = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ResultOfSum)));
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S125:Sections of code should not be commented out", Justification = "Explanation of Lambda syntax with comment")]
        public MultiThreadingDemoPage()
        {
            InitializeComponent();
            progressReporter = new((x) => ProgressOfSum = x);
            /* entspricht
             *  private void namenlos(int x)
             *  {
             *      ProgressOfSum = x;
             *  }
             */
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).IsEnabled = false;
            // Alle rectangles werden eingefärbt
            foreach (Rectangle item in wrpRects.Children)
                item.Fill = Brushes.OrangeRed;

            Task[] thingsToDo = new Task[4];

            // Tasks werden vorbereitet, aber nicht gestartet.
            thingsToDo[0] = new Task(() => timedRecolor(rctAlpha));
            thingsToDo[1] = new Task(() => timedRecolor(rctBravo));
            thingsToDo[2] = new Task(() => timedRecolor(rctCharly));
            thingsToDo[3] = new Task(() => timedRecolor(rctDelta));

            // Alle Tasks werden gestartet

            foreach (var item in thingsToDo)
            {
                item.Start();
            }

            // WhenAll blockiert bis alle Threads fertig sind. Durch das await wird diese Methode geparkt
            // und macht weiter wenn das WhenAll fertig ist
            await Task.WhenAll(thingsToDo);
            (sender as Button).IsEnabled = true;
        }

        private void timedRecolor(Rectangle RectangleToRecolor)
        {
            Random rndGen = new();
            Thread.Sleep(rndGen.Next(3000, 6000)); // zufälliges warten zwischen 3 und 6 sekunden
            //RectangleToRecolor.Fill = Brushes.CornflowerBlue; // verboten! Keine direkten UI operationen aus Threads
            _ = Dispatcher.Invoke(() => RectangleToRecolor.Fill = Brushes.CornflowerBlue); // UI-Thread startet den Lambda, damit darf das UI geändert werden
        }

        private long RandomArraySum(IProgress<double> sumProgress, CancellationToken token)
        {
            byte[] arrayToSum = new byte[800_000_000];
            Random rndGen = new();

            int progresscounter = 0;
            long sumOfArray = 0;
            for (int counter = 0; counter < arrayToSum.Length; counter++)
            {
                arrayToSum[counter] = (byte)rndGen.Next(256);
                if (counter % (arrayToSum.Length / 750) == 0)
                {
                    progresscounter++;
                    sumProgress.Report(progresscounter);
                    if (token.IsCancellationRequested)
                    {
                        return sumOfArray;
                    }
                }
            }

            for (int counter = 0; counter < arrayToSum.Length; counter++)
            {
                sumOfArray += arrayToSum[counter];
                if (counter % (arrayToSum.Length / 250) == 0)
                {
                    progresscounter++;
                    sumProgress.Report(progresscounter);
                    if (token.IsCancellationRequested)
                    {
                        return sumOfArray;
                    }
                }
            }

            return sumOfArray;
        }

        private async void btnStartSumWithProgress_Click(object sender, RoutedEventArgs e)
        {
            ProgressOfSum = 0;
            sumCancellationTokenSource = new();
            ResultOfSum = await Task.Run(() => RandomArraySum(progressReporter, sumCancellationTokenSource.Token), sumCancellationTokenSource.Token);
        }

        private void btnStopSumWithProgress_Click(object sender, RoutedEventArgs e)
        {
            sumCancellationTokenSource.Cancel();
        }
    }
}

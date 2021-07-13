using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ComponentsDemo
{
    /// <summary>
    /// Interaction logic for MultiThreadingDemoPage.xaml
    /// </summary>
    public partial class MultiThreadingDemoPage : Page, INotifyPropertyChanged
    {

        // Fields für die Threads, falls wir später noch zugriff darauf brauchen. z.B. um nachzuschauen ob er fertig ist
        private Task threadA;
        private Task threadB;
        private Task threadC;
        private Task threadD;

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


        public MultiThreadingDemoPage()
        {
            InitializeComponent();
            progressReporter = new((x) => ProgressOfSum = x);
            /* entspricht
                private void namenlos(int x)
                {
                    ProgressOfSum = x;
                }
             */
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).IsEnabled = false;
            // Alle rectangles werden eingefärbt
            foreach (Rectangle item in wrpRects.Children)
                item.Fill = Brushes.OrangeRed;

            // Tasks werden vorbereitet, aber nicht gestartet.
            threadA = new Task(() => timedRecolor(rctAlpha));
            threadB = new Task(() => timedRecolor(rctBravo));
            threadC = new Task(() => timedRecolor(rctCharly));
            threadD = new Task(() => timedRecolor(rctDelta));

            // Alle Tasks werden gestartet
            threadA.Start();
            threadB.Start();
            threadC.Start();
            threadD.Start();

            // WhenAll blockiert bis alle Threads fertig sind. Durch das await wird diese Methode geparkt
            // und macht weiter wenn das WhenAll fertig ist
            await Task.WhenAll(threadA, threadB, threadC, threadD);
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

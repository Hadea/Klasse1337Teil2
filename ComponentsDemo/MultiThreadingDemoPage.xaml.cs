using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComponentsDemo
{
    /// <summary>
    /// Interaction logic for MultiThreadingDemoPage.xaml
    /// </summary>
    public partial class MultiThreadingDemoPage : Page
    {

        // Fields für die Threads, falls wir später noch zugriff darauf brauchen. z.B. um nachzuschauen ob er fertig ist
        private Task threadA;
        private Task threadB;
        private Task threadC;
        private Task threadD;

        public MultiThreadingDemoPage()
        {
            InitializeComponent();
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
    }
}

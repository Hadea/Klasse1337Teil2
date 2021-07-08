using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ComponentsDemo
{
    /// <summary>
    /// Interaction logic for UnThreadedDemoPage.xaml
    /// </summary>
    public partial class AsyncAwaitDemoPage : Page
    {
        public AsyncAwaitDemoPage()
        {
            InitializeComponent();
        }

        private void btnBlocking_Click(object sender, RoutedEventArgs e)
        {
            tblOutput.Text = "button wurde gestartet"; // wird nicht gesehen
            Thread.Sleep(5000);
            tblOutput.Text = "Sind halb fertig"; // wird nicht gesehen
            Thread.Sleep(5000);
            tblOutput.Text = "Erledigt"; // nur der letzte stand wird gezeichnet da das Auffrischen des UI nach dem Durchlauf der Funktion gestartet wird.
        }

        private async void btnAsync_Click(object sender, RoutedEventArgs e)
        {
            tblOutput.Text = "button wurde gestartet";// ist sichtbar
            // Die Aufgabe wird gestartet und die Methode beendet, dadurch kann das UI weiterarbeiten
            // Sobald die Aufgabe erledigt ist wird beim await weitergemacht
            await Task.Delay(5000);
            tblOutput.Text = "Sind halb fertig";
            tblOutput.Text = await WaitAndPrintAsync();
            tblOutput.Text = await Task.Run( () => WaitAndPrint());
        }

        async Task<string> WaitAndPrintAsync()
        {
            await Task.Delay(5000);
            return "fast Fertig";
        }

        string WaitAndPrint()
        {
            Thread.Sleep(5000);
            return "Fertig nummer zwei";
        }
    }
}

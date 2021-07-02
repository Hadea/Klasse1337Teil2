using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
    /// Interaction logic for TextBlockDemoPage.xaml
    /// </summary>
    public partial class TextBlockDemoPage : Page
    {
        private int mAddedElementCounter;
        public TextBlockDemoPage()
        {
            InitializeComponent();
        }

        private void btnAddRow_Click(object sender, RoutedEventArgs e)
        {
            Run elementToAdd = new Run();
            elementToAdd.Foreground = Brushes.DarkGreen;
            elementToAdd.Text = "Neues Element " + ++mAddedElementCounter;
            tblContent.Inlines.Add(new LineBreak());
            tblContent.Inlines.Add(elementToAdd);
        }

        private void Hyperlink_Click(object sender, RequestNavigateEventArgs e)
        {
            // startet einen neuen Prozess für die angegebene Datei. Anhand des Aufbaus des Dateinamens
            // wird dann entschieden welches Programm die datei öffnet oder ob sie selbst startfähig ist
            _ = Process.Start(new ProcessStartInfo() {FileName = e.Uri.ToString(), UseShellExecute = true });

            // Um allen folgenden Eventhandlern zu signalisieren das das Event bereits erledigt ist
            // wird das Handled auf true gesetzt. Folgende Eventhandler überspringen dann ihren code
            e.Handled = true;
        }
    }
}

using System.Drawing; // nicht serienmässig dabei, kann aber über das Paket "System.Drawing.Common" hinzugefügt werden
using System.Windows;
using System.Windows.Controls;

namespace ComponentsDemo
{
    /// <summary>
    /// Interaction logic for FormattingExercisePage.xaml
    /// </summary>
    public partial class FormattingExercisePage : Page
    {
        public FormattingExercisePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Methode um einen Screenshot der Anwendung zu machen und als "Screenshot.bmp" abzuspeichern
        /// </summary>
        /// <param name="sender">unused</param>
        /// <param name="e">unused</param>
        private void btnScreenshot_Click(object sender, RoutedEventArgs e)
        {
            // Ein leeres Bitmap-Objekt erstellen mit der Anangsgrösse unseres Fensters
            // Der Code ist innerhalb der Page, welche NICHT die fenstergrösse beeinflusst, sondern nur
            // den Frame füllt, deshalb lesen wir die werte direkt vom MainWindow.
            // Die Multiplikation mit 1.5 entsteht durch die Desktop-Skalierung welche bei mir 150% ist
            // CopyFromScreen rechnet in Physikalischen Pixeln
            // WPF rechnet in Logischen (plattformunabhängigen) Bild-Koordinaten welche nicht physischen pixeln entsprechen muss
            using (Bitmap bmp = new Bitmap((int)(Application.Current.MainWindow.Width * 1.5d), (int)(Application.Current.MainWindow.Height * 1.5d)))
            {
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.CopyFromScreen((int)(Application.Current.MainWindow.Left * 1.5d), (int)(Application.Current.MainWindow.Top * 1.5d), 0, 0, bmp.Size);
                    bmp.Save("Screenshot.bmp");
                }
            }
        }
    }
}

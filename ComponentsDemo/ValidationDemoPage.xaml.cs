using System;
using System.Collections.Generic;
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
    /// Interaction logic for ValidationDemoPage.xaml
    /// </summary>
    public partial class ValidationDemoPage : Page
    {
        public ValidationDemoPage()
        {
            InitializeComponent();
        }

        private string _alphaText;

        public string AlphaText
        {
            get => _alphaText;
            set
            {
                if (_alphaText == value) return;
                // in diesem beispiel wird der neue Wert gespeichert egal ob validierung erfolgreich war
                // sollte das nicht gewünscht sein einfach hinter die Überprüfung schieben.
                // Vorsicht: Wenn die zuweisung nach der überprüfung stattfindet auf Bindungsrichtung (Mode) achten
                _alphaText = value;
                // Die Exeption wird benutzt um dem gebundenen Element mitzuteilen ob ein gültiger Wert empfangen wurde
                // Validierung über Exceptions kann zu kleinen verzögerungen führen sobald die exception geworfen wird
                if (_alphaText.Length < 4) throw new FormatException();
            }
        }

        public string BravoText { get; set; }
    }
}

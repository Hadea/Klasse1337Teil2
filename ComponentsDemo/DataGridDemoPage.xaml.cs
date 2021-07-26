using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for DataGridDemoPage.xaml
    /// </summary>
    public partial class DataGridDemoPage : Page
    {
        public ObservableCollection<Content> ContentList { get; init; } = new();
        private ICollectionView contentListView { get; init; }
        public string FilterText { get; set; } = string.Empty; // beim start ein leerer string damit im Filter kein null-test nötig ist
        public DataGridDemoPage()
        {
            InitializeComponent();
            ContentList.Add(new Content() {Name = "Hans", IsReady = true });
            ContentList.Add(new Content() {Name = "Jaqueline", IsReady = true });
            ContentList.Add(new Content() {Name = "Peter", IsReady = true });
            ContentList.Add(new Content() {Name = "Karen", IsReady = false });
            ContentList.Add(new Content() {Name = "Kevin", IsReady = false });
            ContentList.Add(new Content() {Name = "Petra", IsReady = true });

            // erstellt eine Ansicht für eine Collection mit der wir rein visuell die Inhalte manipulieren und filtern können
            // die originaldaten werden dabei nicht angetastet
            contentListView = CollectionViewSource.GetDefaultView(ContentList);

            // Zum filtern wird hier ein Lambda verwendet welches für jedes element in der Collection gestartet wird
            // sobald der Befehl Refresh gestartet wurde.
            // Dieser filter nutzt den Inhalt eines Properties (durch TextBox gebunden und befüllt) mit dem es vergleicht
            // und bool zurückgibt. Wenn der bool true ist wird das Element angezeigt, andernfalls nicht.
            contentListView.Filter = new Predicate<object>(x => (x as Content).Name.Contains(FilterText));
        }

        private void tbxSearch_KeyUp(object sender, KeyEventArgs e)
        {
            contentListView.Refresh(); //aktualisiert die ansicht und wendet dabei die filter an
        }
    }

    public class Content
    {
        public string Name { get; set; }
        public bool IsReady { get; set; }
    }
}

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

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
            ComponentsDemo.Content.FillWithDummyData(ContentList);

            // erstellt eine Ansicht für eine Collection mit der wir rein visuell die Inhalte manipulieren und filtern können
            // die originaldaten werden dabei nicht angetastet
            contentListView = CollectionViewSource.GetDefaultView(ContentList);

            // Zum filtern wird hier ein Lambda verwendet welches für jedes element in der Collection gestartet wird
            // sobald der Befehl Refresh gestartet wurde.
            // Dieser filter nutzt den Inhalt eines Properties (durch TextBox gebunden und befüllt) mit dem es vergleicht
            // und bool zurückgibt. Wenn der bool true ist wird das Element angezeigt, andernfalls nicht.
            contentListView.Filter = new Predicate<object>(
                (ItemToFilter) =>
                FilterText != "" && (ItemToFilter as Content).Name.Contains(FilterText)
                );

            DataContext = this;
        }

        private void tbxSearch_KeyUp(object sender, KeyEventArgs e)
        {
            contentListView.Refresh(); //aktualisiert die ansicht und wendet dabei die filter an
        }
    }

    public class Content
    {
        /// <summary>
        /// Fills an <see cref="ObservableCollection{Content}"/> with some dummy content.
        /// </summary>
        /// <param name="ListToFill">The Collection to be filled</param>
        public static void FillWithDummyData(ObservableCollection<Content> ListToFill)
        {
            ListToFill.Add(new Content() { Name = "Hans", IsReady = true });
            ListToFill.Add(new Content() { Name = "Jaqueline", IsReady = true });
            ListToFill.Add(new Content() { Name = "Peter", IsReady = true });
            ListToFill.Add(new Content() { Name = "Karen", IsReady = false });
            ListToFill.Add(new Content() { Name = "Kevin", IsReady = false });
            ListToFill.Add(new Content() { Name = "Petra", IsReady = true });
        }
        public string Name { get; set; }
        public bool IsReady { get; set; }

    }
}

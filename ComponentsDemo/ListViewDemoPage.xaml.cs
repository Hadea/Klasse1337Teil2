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
    /// Interaction logic for ListViewDemoPage.xaml
    /// </summary>
    public partial class ListViewDemoPage : Page
    {
        private readonly List<ListElement> mListContent = new();

        public ListViewDemoPage()
        {
            InitializeComponent();
#if DEBUG
            AddDebugStuff();
#endif

            mListContent.Add(new ListElement() { Name = "Hans", Number = 11 });
            mListContent.Add(new ListElement() { Name = "Peter", Number = 22 });
            mListContent.Add(new ListElement() { Name = "Ingrid", Number = 33 });
            mListContent.Add(new ListElement() { Name = "Maria", Number = 44 });
            lvContent.ItemsSource = mListContent;
        }

        private void AddDebugStuff()
        {
            Grid currentGrid = Content as Grid;
            currentGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(25) });
            Label debugLabel = new();
            debugLabel.Content = "Debug-stuff";
            Grid.SetRow(debugLabel, currentGrid.RowDefinitions.Count - 1);
            Grid.SetColumn(debugLabel, 0);
            currentGrid.Children.Add(debugLabel);
        }

        private void lvContent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tblContent.Inlines.Clear();
            foreach (var item in lvContent.SelectedItems)
            {
                tblContent.Inlines.Add(new Run((item as ListElement).Name));
                tblContent.Inlines.Add(new LineBreak());
            }
            
        }
    }

    class ListElement
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Number { get; set; }
    }
}

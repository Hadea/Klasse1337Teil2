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
    /// Interaction logic for GridDemoPage.xaml
    /// </summary>
    public partial class GridDemoPage : Page
    {
        public GridDemoPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // Button erstellen, da im XAML keine existieren
            for (int counterRow = 0; counterRow < grdAlpha.RowDefinitions.Count; counterRow++)
            {
                for (int counterCol = 0; counterCol < grdAlpha.ColumnDefinitions.Count; counterCol++)
                {
                    // Button im RAM erstellen
                    Button freshButton = new() 
                        { Content = counterRow * grdAlpha.ColumnDefinitions.Count + counterCol};

                    // Dem Grid die Position des Buttons mitteilen
                    Grid.SetColumn(freshButton, counterCol);
                    Grid.SetRow(freshButton, counterRow);
                    // Dem Grid sagen das der erstellte Button ein Unterelement ist
                    grdAlpha.Children.Add(freshButton);
                }
            }


            foreach (Button item in grdAlpha.Children)
            {
                item.Background = Brushes.CornflowerBlue;
            }
        }
    }
}

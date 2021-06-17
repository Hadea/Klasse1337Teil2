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
    /// Interaction logic for ButtonDemoPage.xaml
    /// </summary>
    public partial class ButtonDemoPage : Page
    {
        private int mClickCount;

        public ButtonDemoPage()
        {
            InitializeComponent();
            lblHoverInfo.Content = "stuff";
        }

        private void btnFirstButton_Click(object sender, RoutedEventArgs e)
        {
            lblClickInfo.Content = (sender as Button).Name + $" Wurde {++mClickCount} mal geklickt";
        }

        private void btnFirstButton_MouseEnter(object sender, MouseEventArgs e)
        {
            lblHoverInfo.Content = "Hovern wurde gestartet";
        }

        private void btnFirstButton_MouseLeave(object sender, MouseEventArgs e)
        {
            lblHoverInfo.Content = "Hovern wurde beendet";
        }
    }
}

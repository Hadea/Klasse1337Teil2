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
    /// Interaction logic for ImageDemoPage.xaml
    /// </summary>
    public partial class ImageDemoPage : Page
    {
        public ImageDemoPage()
        {
            InitializeComponent();
        }

        private void btnVisibilityChange_Click(object sender, RoutedEventArgs e)
        {
            imgVisibility.Visibility = imgVisibility.Visibility switch
            {
                Visibility.Visible => Visibility.Hidden,
                Visibility.Hidden => Visibility.Collapsed,
                _ => Visibility.Visible
            };
            tblVisibility.Text = imgVisibility.Visibility.ToString();
        }
    }
}

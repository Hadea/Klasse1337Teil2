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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStackpanel_Click(object sender, RoutedEventArgs e)
        {
            frmContent.Navigate(new StackpanelDemoPage());
        }

        private void btnGrid_Click(object sender, RoutedEventArgs e)
        {
            frmContent.Navigate(new GridDemoPage());
        }

        private void btnButton_Click(object sender, RoutedEventArgs e)
        {
            frmContent.Navigate(new ButtonDemoPage());
        }

        private void btnTextBox_Click(object sender, RoutedEventArgs e)
        {
            frmContent.Navigate(new TextBoxDemoPage());
        }

        private void btnTextBlock_Click(object sender, RoutedEventArgs e)
        {
            frmContent.Navigate(new TextBlockDemoPage());
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            frmContent.Navigate(new ImageDemoPage());
        }

        private void btnSlider_Click(object sender, RoutedEventArgs e)
        {
            frmContent.Navigate(new SliderDemoPage());
        }

        private void btnScrollBar_Click(object sender, RoutedEventArgs e)
        {
            frmContent.Navigate(new ScrollBarDemoPage());
        }

        private void btnColor_Click(object sender, RoutedEventArgs e)
        {
            frmContent.Navigate(new ColorDemoPage());
        }

        private void btnRadioCheck_Click(object sender, RoutedEventArgs e)
        {
            frmContent.Navigate(new RadioCheckDemoPage());
        }
    }
}
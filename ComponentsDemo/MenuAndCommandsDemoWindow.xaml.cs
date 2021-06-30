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
using System.Windows.Shapes;

namespace ComponentsDemo
{
    /// <summary>
    /// Interaction logic for MenuAndCommandsDemoWindow.xaml
    /// </summary>
    public partial class MenuAndCommandsDemoWindow : Window
    {
        public MenuAndCommandsDemoWindow()
        {
            InitializeComponent();
        }

        private void CommandNew_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void CommandOpen_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void CommandClose_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void CommandSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("gespeichert");
        }
    }
}

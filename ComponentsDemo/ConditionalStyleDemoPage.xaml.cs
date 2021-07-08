using System;
using System.Collections.Generic;
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
    /// Interaction logic for ConditionalStyleDemoPage.xaml
    /// </summary>
    public partial class ConditionalStyleDemoPage : Page, INotifyPropertyChanged
    {

        public bool RedAlert
        {
            get => _redAlert;
            set
            {
                _redAlert = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RedAlert)));
            }
        }
        private bool _redAlert;

        public event PropertyChangedEventHandler PropertyChanged;

        public ConditionalStyleDemoPage()
        {
            InitializeComponent();
        }

        private void btnFlip_Click(object sender, RoutedEventArgs e)
        {
            RedAlert = !RedAlert;
        }
    }
}

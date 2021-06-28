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
    /// Interaction logic for BindingsDemoPage.xaml
    /// </summary>
    public partial class BindingsDemoPage : Page, INotifyPropertyChanged
    {
        private double _sliderValue;

        public event PropertyChangedEventHandler PropertyChanged;

        public double mSliderValue
        {
            get => _sliderValue;

            set
            {
                if (_sliderValue != value)
                {
                    _sliderValue = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("mSliderValue"));
                }
            }
        }


        public BindingsDemoPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void btnReadSlider_Click(object sender, RoutedEventArgs e)
        {
            lblSliderValueButton.Content = mSliderValue;
        }

        private void btnSetOne_Click(object sender, RoutedEventArgs e)
        {
            mSliderValue = 1d;
        }
    }
}

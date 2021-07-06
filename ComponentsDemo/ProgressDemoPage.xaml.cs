using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ProgressDemoPage.xaml
    /// </summary>
    public partial class ProgressDemoPage : Page, INotifyPropertyChanged
    {
        public ProgressDemoPage()
        {
            InitializeComponent();
            StatusMessages.Add("Alpha");
            StatusMessages.Add("Bravo");
            StatusMessages.Add("Charly");
            StatusMessages.Add("Delta");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void CommandStartStop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ProgressValue = 0;
            for (int counter = 0; counter < 100; counter++)
            {
                Thread.Sleep(50);
                ProgressValue += 1;
            }
        }

        public double ProgressValue
        {
            get => _progress;
            set
            {
                _progress = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProgressValue)));
            }
        }
        private double _progress;

        public ObservableCollection<string> StatusMessages { get; init; } = new();
    }
}

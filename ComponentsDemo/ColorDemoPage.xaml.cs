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

    //todo: eigene brushes erstellen
    /// <summary>
    /// Interaction logic for ColorDemoPage.xaml
    /// </summary>
    public partial class ColorDemoPage : Page
    {
        private int mCurrentColorIndex;
        private readonly List<(string, SolidColorBrush)> brushList = new();
        public ColorDemoPage()
        {
            InitializeComponent();

            System.Reflection.PropertyInfo[] proplist = typeof(Brushes).GetProperties();
            foreach (System.Reflection.PropertyInfo item in proplist)
                brushList.Add((item.Name, item.GetValue(null) as SolidColorBrush));
            rainbowRectangleCreation();
        }

        private void rainbowRectangleCreation()
        {
            foreach ((string, SolidColorBrush) item in brushList)
                _ = spAllColors.Children.Add(new Rectangle() { Fill = item.Item2 });
        }

        private void btnColorSwitch_Click(object sender, RoutedEventArgs e)
        {
            Button s = sender as Button;
            s.Foreground = brushList[mCurrentColorIndex].Item2;
            s.Content = brushList[mCurrentColorIndex].Item1;
            mCurrentColorIndex = mCurrentColorIndex == brushList.Count - 1 ? 0 : mCurrentColorIndex + 1;
        }
    }
}
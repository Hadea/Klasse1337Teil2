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
using System.Drawing;

namespace ComponentsDemo
{
    /// <summary>
    /// Interaction logic for FormattingExercisePage.xaml
    /// </summary>
    public partial class FormattingExercisePage : Page
    {
        public FormattingExercisePage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (Bitmap bmp = new Bitmap((int)(Application.Current.MainWindow.Width * 1.5d), (int)(Application.Current.MainWindow.Height * 1.5d)))
            {
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.CopyFromScreen((int)(Application.Current.MainWindow.Left*1.5d), (int)(Application.Current.MainWindow.Top*1.5d), 0, 0, bmp.Size);
                    bmp.Save("Screenshot.bmp");
                }
            }
        }
    }
}

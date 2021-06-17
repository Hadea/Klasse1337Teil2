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
    /// Interaction logic for TextBoxDemoPage.xaml
    /// </summary>
    public partial class TextBoxDemoPage : Page
    {
        public TextBoxDemoPage()
        {
            InitializeComponent();
        }

        private void tbxInput_KeyUp(object sender, KeyEventArgs e)
        {
            if ((sender as TextBox).Text.Length < 5)
            {
                lblOutput.Content = "Zu Kurz";
                lblOutput.Foreground = Brushes.Red;
            }
            else
            {
                lblOutput.Content = "Länge ist OK";
                lblOutput.Foreground = Brushes.Green;
            }
        }

        private void tbxInputB_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text.Length < 5)
            {
                lblOutput.Content = "Zu Kurz";
                lblOutput.Foreground = Brushes.Red;
            }
            else
            {
                lblOutput.Content = "Länge ist OK";
                lblOutput.Foreground = Brushes.Green;
            }
        }

        private void btnPassword_Click(object sender, RoutedEventArgs e)
        {
            lblPasswordResult.Content = (pbxPass.Password == "1337" ? "Login erfolgreich" : "Login fehler");
        }
    }
}

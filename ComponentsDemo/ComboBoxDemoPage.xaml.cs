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
    /// Interaction logic for ComboBoxDemoPage.xaml
    /// </summary>
    public partial class ComboBoxDemoPage : Page
    {
        private readonly List<string> mCharlyStringList;

        public ComboBoxDemoPage()
        {
            InitializeComponent();

            mCharlyStringList = new();
            mCharlyStringList.Add("asdf");
            mCharlyStringList.Add("yxcv");
            mCharlyStringList.Add("qwer");
            
            // Die Liste wird eingelesen und es werden automatisch die ComboboxItems erstellt
            // Das passiert nur ein einziges mal beim zuweisen sodass nachträgliche änderungen
            // innerhalb der mCharlyStringList nicht berücksichtigt werden
            cmbCharly.ItemsSource = mCharlyStringList; 
        }

        private void cmbAlpha_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tblAlphaSelection.Text = (sender as ComboBox).SelectedIndex.ToString() + " " + (sender as ComboBox).SelectedItem.ToString();
        }

        private void cmbBravo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((sender as ComboBox).SelectedItem as ComboBoxItem).Content != null)
            {
                tblBravoSelection.Text = ((((sender as ComboBox).SelectedItem as ComboBoxItem).Content as StackPanel).Children[1] as TextBlock).Text;
            }
        }

        private void cmbCharly_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // die ComboBox welche über eine Liste gefüllt wurde liefert die ID welche aus der Liste gewählt wurde
            tblCharlySelection.Text = mCharlyStringList[(sender as ComboBox).SelectedIndex];
        }
    }
}

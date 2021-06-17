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
    /// Interaction logic for TextBlockDemoPage.xaml
    /// </summary>
    public partial class TextBlockDemoPage : Page
    {
        private int mAddedElementCounter;
        public TextBlockDemoPage()
        {
            InitializeComponent();
        }

        private void btnAddRow_Click(object sender, RoutedEventArgs e)
        {
            Run elementToAdd = new Run();
            elementToAdd.Foreground = Brushes.DarkGreen;
            elementToAdd.Text = "Neues Element " + ++mAddedElementCounter;
            tblContent.Inlines.Add(new LineBreak());
            tblContent.Inlines.Add(elementToAdd);
        }
    }
}

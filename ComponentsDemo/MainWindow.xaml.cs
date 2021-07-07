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
            DataContext = this;
            NavigateCommand = new DelegateCommand(navigateToPage, canNavigate);
        }

        public ICommand NavigateCommand { get; init; }

        private bool canNavigate(object pageName)
        {
            if (pageName is null) return false;
            Type choosenClass = Type.GetType("ComponentsDemo." + pageName.ToString());
            return choosenClass is not null && ( choosenClass.IsSubclassOf(typeof(Page)) || choosenClass.IsSubclassOf(typeof(Window)));
        }
        private void navigateToPage(object pageName)
        {
            Type choosenClass = Type.GetType("ComponentsDemo." + pageName.ToString());
            if (choosenClass.IsSubclassOf(typeof(Page)))
                _ = frmContent.Navigate(Activator.CreateInstance(choosenClass));
            else if (choosenClass.IsSubclassOf(typeof(Window)))
            {
                Window w = (Window)Activator.CreateInstance(choosenClass);
                w.Show();
            }
        }
    }
}

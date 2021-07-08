using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            DataContext = this; // setzt den datenkontext damit direkt gebunden werden kann ohne ElementName anzugeben
            NavigateCommand = new DelegateCommand(navigateToPage, canNavigate); //erstellt ein Command welches die angegebenen Methoden startet
        }

        public ICommand NavigateCommand { get; init; }

        private bool canNavigate(object pageName)
        {
            if (pageName is null) return false; // early exit wenn kein Parameter angegeben ist
            Type choosenClass = Type.GetType("ComponentsDemo." + pageName.ToString()); // Anhand des übergebenen Strings den Type finden.
            // prüfen ob der Type gefunden wurde und falls ja, ob es einer der beiden gewünschten ist
            return choosenClass is not null && ( choosenClass.IsSubclassOf(typeof(Page)) || choosenClass.IsSubclassOf(typeof(Window)));
        }

        private void navigateToPage(object pageName)
        {
            Type choosenClass = Type.GetType("ComponentsDemo." + pageName.ToString()); // Type anhand des Parameter heraussuchen
            if (choosenClass.IsSubclassOf(typeof(Page))) // wenn es eine Page ist dann erstellen und im Frame einhängen
                _ = frmContent.Navigate(Activator.CreateInstance(choosenClass));
            else if (choosenClass.IsSubclassOf(typeof(Window))) // wenn es ein Window ist, erstellen und zeigen
            {
                Window w = (Window)Activator.CreateInstance(choosenClass);
                w.Show();
            }
        }
    }
}

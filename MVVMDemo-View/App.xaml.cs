using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMDemo_View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            //Wenn über den Konstruktor des Fensters das ViewModel zugewiesen werden soll kann das hier passieren
            // hier könnte man auch dem ViewModel die Logik mitgeben sodass hier die einzige stelle ist in der
            // die abhängigkeit der Projekte verwaltet wird
            // StartupUri muss dann im App.xaml entfernt werden

            //Window w = new MainWindow(new MVVMDemo_ViewModel.MainWindow_ViewModel());
            //w.Show();
        }
    }
}

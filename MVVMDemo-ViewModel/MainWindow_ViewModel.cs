using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MVVMDemo_ViewModel
{
    public class MainWindow_ViewModel : INotifyPropertyChanged //Immer wenn ihr das Interface verwendet habt ihr ein ViewModel
    {
        public MainWindow_ViewModel()
        {
            // startet/benutzt logik

            // vorbefüllen der Liste mit beispieldaten, die sollten durch die Logik kommen
            ListOfElements.Add(new Element_ViewModel() { Name = "Alpha", Address = "AlphaAddress" });
            ListOfElements.Add(new Element_ViewModel() { Name = "Bravo", Address = "BravoAddress" });
            ListOfElements.Add(new Element_ViewModel() { Name = "Charly", Address = "CharlyAddress" });
            
            // Das Property für ein Command wird hier mit einer Passenden selbstgeschriebenen Klasse befüllt
            // AddUser_Command würde passen, bräuchte aber zusatzinformationen zur Liste
            // Das DelegateCommand ist eine leere hülle welcher wir erst die Funktionalität geben
            // die Funktionalität schreiben wir hier sodass wir uns weniger gedanken über die Referenzen
            // machen müssen
            AddUser = new DelegateCommand(
                (x) => ListOfElements.Add(new() {Name = x.ToString(), Address = "unknown" }), // funktionalität kann als Lambda oder als Funktion weitergegeben werden
                AddUser_CanExecute );

            ModifyUser = new DelegateCommand((x) => (x as Element_ViewModel).Name = "Geändert");
        }

        /// <summary>
        /// Prüft ob ein Nutzer mit dem mitgelieferten Namen erstellt werden könnte
        /// </summary>
        /// <param name="parameter"><see cref="String"/> mit dem Namen des Nutzers</param>
        /// <returns>True wenn der Name den Regeln entspricht, andernfalls false</returns>
        private bool AddUser_CanExecute(object parameter)
        {
            if (parameter is null) return false;
            if ((parameter as string).Length == 0) return false;

            return true;
        }

        // Property für ein Command an das eine Bindung erstellt werden kann
        // Durch die Verwendung des Interface kann dieses Property mit jeder passenden Klasse befüllt werden
        // Dadurch könnte die art wie ein Nutzer hinzugefügt wird sogar im laufenden betrieb geändert werden
        public ICommand AddUser { get; set; } 
        public ICommand ModifyUser { get; set; }

        public double AmountSelected
        {
            get { return _amountSelected; }
            set
            {
                _amountSelected = value;
                AmountDisplay = "Gehalt" + _amountSelected.ToString("C2");
            }
        }
        private double _amountSelected;

        public event PropertyChangedEventHandler PropertyChanged;

        public string AmountDisplay
        {
            get { return _amountDisplay; }
            set
            {
                _amountDisplay = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AmountDisplay)));
            }
        }
        private string _amountDisplay;

        // Die Observable collection gibt beim hinzufügen oder löschen von elementen selbst dem UI bescheid
        public ObservableCollection<Element_ViewModel> ListOfElements { get; init; } = new();
    }
}

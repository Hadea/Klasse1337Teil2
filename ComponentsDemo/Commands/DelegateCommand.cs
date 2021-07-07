using System;
using System.Windows.Input;

namespace ComponentsDemo
{

    class DelegateCommand : ICommand // Kompatibel mit Command aus XAML
    {
        public event EventHandler CanExecuteChanged;

        // hier wird die eigendliche funktionalität hinterlegt
        private readonly Action<object> _executed;
        private readonly Func<object, bool> _canExecute;

        public DelegateCommand(Action<object> CommandToExecute, Func<object, bool> CanCommandExecute = null )
        {
            _executed = CommandToExecute;
            _canExecute = CanCommandExecute;
        }

        // startet die Methode welche im Konstruktor mitgeliefert wurde
        public bool CanExecute(object parameter)
        {
            // wenn es keine Testmethode gibt soll das Command immer als funktionsfähig betrachtet werden
            if (_canExecute is null)
            {
                return true;
            }
            else
            {
                // andernfalls wird die mitgelieferte Methode gestartet um die Funktionsfähigkeit zu testen
                return _canExecute(parameter);
            }
        }

        // startet die Methode welche im Konstruktor mitgeliefert wurde
        public void Execute(object parameter)
        {
            // falls jemand keine Methode für Execute im Konstruktor mitgeliefert hat wird beim versuch der
            // ausführung eine exception geworfen
            if (_executed is null) throw new NotImplementedException();
            _executed(parameter);
        }
    }
}

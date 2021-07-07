using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMDemo_ViewModel
{
    class AddUser_Command : ICommand
    {
        private ObservableCollection<Element_ViewModel> _listOfElements;

        public AddUser_Command(ObservableCollection<Element_ViewModel> ListOfElements)
        {
            _listOfElements = ListOfElements;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is null)
            {
                _listOfElements.Add(new Element_ViewModel() { Name = "New User", Address = "none" });
            }
            else
            {
                _listOfElements.Add(new Element_ViewModel() { Name = parameter.ToString(), Address = "none" });
            }
        }
    }
}

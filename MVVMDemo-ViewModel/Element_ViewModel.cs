using System.ComponentModel;

namespace MVVMDemo_ViewModel
{
    public class Element_ViewModel : INotifyPropertyChanged // Mit diesem interface ist es immer ein ViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                // änderungen im Namen werden an das UI kommuniziert
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }
        private string _name;

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Address)));
            }
        }
        private string _address;
    }
}

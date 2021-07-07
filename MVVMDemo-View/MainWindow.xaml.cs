using System.Windows;

namespace MVVMDemo_View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Der Kontext ist der einzige Code der hier in der Code-Behind datei noch erlaubt ist.
            // Dies könnte man aber durchaus auch in XAML erledigen
            DataContext = new MVVMDemo_ViewModel.MainWindow_ViewModel();
        }
    }
}

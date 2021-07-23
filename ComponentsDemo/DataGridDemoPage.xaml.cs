using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for DataGridDemoPage.xaml
    /// </summary>
    public partial class DataGridDemoPage : Page
    {
        public ObservableCollection<Content> ContentList { get; init; } = new();
        public DataGridDemoPage()
        {
            InitializeComponent();
            ContentList.Add(new Content() {Name = "Hans", IsReady = true });
            ContentList.Add(new Content() {Name = "Jaqueline", IsReady = true });
            ContentList.Add(new Content() {Name = "Peter", IsReady = true });
            ContentList.Add(new Content() {Name = "Karen", IsReady = false });
            ContentList.Add(new Content() {Name = "Kevin", IsReady = false });
            ContentList.Add(new Content() {Name = "Petra", IsReady = true });
        }
    }

    public class Content
    {
        public string Name { get; set; }
        public bool IsReady { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
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

namespace ZipDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach (string item in Directory.GetFiles("Archives/", "*.zip"))
            {
                ArchiveList.Add(item);
            }
        }
        public ObservableCollection<string> ArchiveList { get; init; } = new();
        public string SelectedArchive
        {
            get => _selectedArchive;
            set
            {
                if (_selectedArchive == value) return;
                _selectedArchive = value;
                if (!File.Exists(_selectedArchive)) throw new FileNotFoundException(_selectedArchive);

                using (ZipArchive zip = ZipFile.OpenRead(_selectedArchive))
                {
                    ContentList.Clear();
                    foreach (ZipArchiveEntry contentFileName in zip.Entries)
                    {
                        ContentList.Add(contentFileName.FullName);
                    }
                }
            }
        }
        private string _selectedArchive;
        public ObservableCollection<string> ContentList { get; init; } = new();
        public string SelectedContent
        {
            get => _selectedContent;
            set
            {
                if (_selectedContent == value) return;
                _selectedContent = value;
                BitmapImage loadedImage = new();

                using (ZipArchive zip = ZipFile.OpenRead(_selectedArchive))
                {
                    var entry = zip.GetEntry(_selectedContent);
                    loadedImage.BeginInit();
                    loadedImage.StreamSource = entry.Open();
                    loadedImage.CacheOption = BitmapCacheOption.OnLoad;
                    loadedImage.EndInit();
                }

                SelectedImage = loadedImage;
            }
        }
        private string _selectedContent;
        public BitmapImage SelectedImage
        {
            get => _selectedImage;
            set
            {
                _selectedImage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedImage)));
            }
        }
        private BitmapImage _selectedImage;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

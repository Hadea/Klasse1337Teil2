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
        public ObservableCollection<string> ArchiveList { get; init; } = new(); // Liste für Archivnamen
        public ObservableCollection<string> ContentList { get; init; } = new(); // Liste für Dateinamen im gewählten Archiv

        public string SelectedArchive
        {
            get => _selectedArchive;
            set
            {
                if (_selectedArchive == value) return; // testet ob sich überhaupt etwas verändert hat, falls nicht: abbruch
                _selectedArchive = value;
                if (!File.Exists(_selectedArchive)) throw new FileNotFoundException(_selectedArchive); // testet ob die angeforderte Datei auch wirklich existiert

                using (ZipArchive zip = ZipFile.OpenRead(_selectedArchive)) // öffnet eine Zip-Datei mit leserechten
                {
                    ContentList.Clear();
                    foreach (ZipArchiveEntry contentFileName in zip.Entries) // Alle einträge im Zip durchgehen
                    {
                        ContentList.Add(contentFileName.FullName); // Nur den Dateinamen der Einträge in einer Auswahlliste ablegen
                    }
                }
            }
        }
        private string _selectedArchive;
        public string SelectedContent
        {
            get => _selectedContent;
            set
            {
                if (_selectedContent == value) return; // testet ob sich überhaupt etwas verändert hat
                _selectedContent = value;
                BitmapImage loadedImage = new(); // neue Bilddaten werden vorbereitet

                using (ZipArchive zip = ZipFile.OpenRead(_selectedArchive)) // Zip-Datei wird geöffnet
                {
                    var entry = zip.GetEntry(_selectedContent); // der gewählte Eintrag im Zip wird geladen
                    loadedImage.BeginInit(); // beginn Bilddaten laden
                    loadedImage.StreamSource = entry.Open(); // Die datei wird direkt als Stream weitergegeben
                    loadedImage.CacheOption = BitmapCacheOption.OnLoad; // sicherstellen das die Daten jetzt schon vollständig aus der Datei geladen werden
                    loadedImage.EndInit(); // ende Bilddaten laden
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

                // Aktualisiert alle Elemente welche sich an das Property "SelectedImage" gebunden haben
                // da die Änderung des Inhaltes innerhalb von C# passiert muss das UI über diese Änderung
                // informiert werden.
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedImage)));
            }
        }
        private BitmapImage _selectedImage;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

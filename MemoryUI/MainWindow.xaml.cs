using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MemoryUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Button mSelectedButtonA; // erste auswahl
        private Button mSelectedButtonB; // zweite auswahl für vergleich
        private DateTime mTimeGameStart; // Wird befüllt wenn das erste Feld umgedreht wird um die Startzeit des Spiels festzuhalten
        private readonly DispatcherTimer mTimer; // Startet zeitbasiert Events. Hier wird er benutzt um die Uhrzeit im UI anzuzeigen.
        private readonly MediaPlayer mMusic;
        private int mMaxPoints; // Wird verwendet um das ende einer Partie zu errechnen.

        /// <summary>
        /// Anzahl der erreichten Paare in diesem Spiel. Änderungen werden mit dem UI syncronisiert.
        /// </summary>
        public int Points
        {
            get => mPoints;
            set
            {
                if (mPoints != value)
                {
                    mPoints = value; // speichern des neuen Punktewerts
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Points)));
                }
            }
        }
        private int mPoints;

        /// <summary>
        /// Anzahl der benötigten Züge. Änderungen werden mit dem UI syncronisiert.
        /// </summary>
        public int Turns
        {
            get => mTurns;
            set
            {
                if (mTurns != value)
                {
                    mTurns = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Turns)));
                }
            }
        }
        private int mTurns;

        /// <summary>
        /// Verstrichene Zeit in Sekunden seit dem ersten Zug
        /// </summary>
        public double ElapsedTime
        {
            get => _elapsedTime;
            set
            {
                if (_elapsedTime != value)
                {
                    _elapsedTime = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ElapsedTime)));
                }
            }
        }
        private double _elapsedTime;

        /// <summary>
        /// Vertikale Anzahl an Karten.
        /// </summary>
        public int VerticalSize
        {
            get => _verticalSize;
            set
            {
                if (_verticalSize != value)
                {
                    _verticalSize = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VerticalSize)));
                    (ResetCommand as Command_Reset).RaiseCanExecuteChanged();
                }
            }
        }
        private int _verticalSize;

        /// <summary>
        /// Horizontale Anzahl an Karten
        /// </summary>
        public int HorizontalSize
        {
            get => _horizontalSize;
            set
            {
                if (_horizontalSize != value)
                {
                    _horizontalSize = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HorizontalSize)));
                    (ResetCommand as Command_Reset).RaiseCanExecuteChanged();
                }
            }
        }
        private int _horizontalSize;

        /// <summary>
        /// Name des aktuellen Spielers
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// Kommando für den Reset-Button welcher das Spiel neu erstellen soll.
        /// </summary>
        public ICommand ResetCommand { get; init; }

        public MainWindow()
        {
            InitializeComponent(); // läd die Inhalte des XAML

            mTimer = new DispatcherTimer(
                new TimeSpan(0, 0, 0, 0, 100), // alle 100 millisekunden (manchmal später, aber nie früher)
                DispatcherPriority.Render, // legt die priorität fest mit welcher der DispatcherTimer überprüft ob er starten muss
                (_, _) => ElapsedTime = (DateTime.Now - mTimeGameStart).TotalSeconds, // der auszuführende befehl
                Dispatcher.CurrentDispatcher);
            mTimer.Stop(); // hält den Timer an damit das UI nicht aktualisiert wird bevor das Spiel startet
            lvImageSets.ItemsSource = Directory.GetDirectories("Images");
            mMusic = new();
            mMusic.Open(new Uri("Audio/ShaolinDub-HarpDubz.mp3", UriKind.Relative));
            ResetCommand = new Command_Reset(this);
        }

        /// <summary>
        /// Löscht das aktuelle Spielfeld und erstellt es neu anhand der Parameter.
        /// Die Gesamtzahl der Felder muss eine gerade Zahl ergeben (Durch 2 teilbar)
        /// </summary>
        /// <param name="Columns">Anzahl der Spalten des Spielfelds</param>
        /// <param name="Rows">Anzahl der Zeilen des Spielfelds</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public void resetGame(int Columns, int Rows, string[] Folders)
        {

            if (Columns * Rows % 2 == 1) throw new ArgumentOutOfRangeException(nameof(Rows), "Columns und Rows muss eine gerade Zahl ergeben"); // beendet die Funktion wenn die Anzahl der Felder ungerade ist
            if (Folders.Length < 1) throw new ArgumentOutOfRangeException(nameof(Folders), "Ordneranzahl ist zu gering");

            mTimer.Stop();
            ElapsedTime = 0;
            Points = 0;
            Turns = 0;
            mMaxPoints = Columns * Rows / 2; // speichert die maximal erreichbare punktzahl für den Spiel-Ende-Test

            // alle dateinamen der ausgewählten ordner laden
            List<string> fileList = new();
            foreach (string FolderName in Folders)
                fileList.AddRange(Directory.GetFiles(FolderName));

            // zufällige dateien aus der liste auswählen und als bitmap-paar speichern
            Random rndGen = new();
            List<BitmapImage> imageListWithPairs = new();
            for (int counter = 0; counter < mMaxPoints; counter++)
            {
                int randomNumber = rndGen.Next(fileList.Count);
                BitmapImage newBitmap = new();
                newBitmap.BeginInit();// füllen des Bildes starten
                newBitmap.UriSource = new Uri(Directory.GetParent(Environment.CommandLine).FullName + "//" + fileList[randomNumber]);// bildinhalt aus datei laden
                newBitmap.EndInit();// füllen des Bildes finalisieren
                imageListWithPairs.Add(newBitmap);
                imageListWithPairs.Add(newBitmap);
                fileList.RemoveAt(randomNumber);
            }

            // spielfeld leeren
            grdField.Children.Clear();
            grdField.RowDefinitions.Clear();
            grdField.ColumnDefinitions.Clear();

            GridLength cellSize = new(100); // kantenlänge einer Zelle festlegen

            // rows hinzufügen
            for (int counter = 0; counter < Rows; counter++)
            {
                RowDefinition newRow = new() { Height = cellSize };
                grdField.RowDefinitions.Add(newRow);
            }

            // cols hinzufügen (etwas kürzere schreibweise als bei den rows)
            for (int counter = 0; counter < Columns; ++counter)
                grdField.ColumnDefinitions.Add(new() { Width = cellSize });

            // alle zellen durchgehen
            for (int rowCounter = 0; rowCounter < Rows; rowCounter++)
            {
                for (int colCounter = 0; colCounter < Columns; colCounter++)
                {
                    // Button erstellen
                    Button newButton = new();
                    int chosenImageID = rndGen.Next(imageListWithPairs.Count); // zufällige zahl zwischen 0 und kleiner als listenlänge ziehen
                    newButton.Content = new Image() { Source = imageListWithPairs[chosenImageID], Visibility = Visibility.Collapsed }; // bild anhand der zahl aus der liste lesen und dem button zuweisen
                    imageListWithPairs.RemoveAt(chosenImageID); // bild aus der liste entfernen

                    //Grid-Position des button einstellen
                    Grid.SetColumn(newButton, colCounter);
                    Grid.SetRow(newButton, rowCounter);
                    grdField.Children.Add(newButton);// Button in den Grid-Children eintragen
                }
            }// ende alles durchgehen
        }
        private void btnField_Click(object sender, RoutedEventArgs e)
        {
            if (!mTimer.IsEnabled) // testet ob das Spiel gerade gestartet wurde
            {
                mTimeGameStart = DateTime.Now; // startzeit speichern
                mTimer.Start(); // DispatcherTimer starten damit das UI die aktuelle Zeitdifferenz anzeigt
            }

            #region Zwei ungleiche aufgedeckt und einen dritten gewählt
            if (mSelectedButtonA is not null && mSelectedButtonB is not null)
            {
                // zwei unterschiedliche ausgewählt gewesen welche noch offen sind

                // bereits gewählte wieder verstecken
                (mSelectedButtonA.Content as Image).Visibility = Visibility.Collapsed;
                (mSelectedButtonB.Content as Image).Visibility = Visibility.Collapsed;

                // auswahl aufheben
                mSelectedButtonB = null;
                mSelectedButtonA = sender as Button;
                (mSelectedButtonA.Content as Image).Visibility = Visibility.Visible;
                return;
            }
            #endregion

            #region Erster Button aufgedeckt
            if (mSelectedButtonA is null)
            {
                // erster von zwei
                mSelectedButtonA = sender as Button;
                (mSelectedButtonA.Content as Image).Visibility = Visibility.Visible;
                return;
            }
            #endregion

            Turns++;
            // falls zweimal auf den gleichen button geklickt wurde wird dieser direkt zurückgedreht
            if (sender == mSelectedButtonA)
            {
                (mSelectedButtonA.Content as Image).Visibility = Visibility.Collapsed;
                mSelectedButtonA = null;
                return;
            }
            mSelectedButtonB = sender as Button;
            (mSelectedButtonB.Content as Image).Visibility = Visibility.Visible;

            // zweiter wird aufgedeckt und verglichen
            if ((mSelectedButtonA.Content as Image).Source == (mSelectedButtonB.Content as Image).Source)
            {
                mSelectedButtonA.IsEnabled = false;
                mSelectedButtonB.IsEnabled = false;
                mSelectedButtonA = null;
                mSelectedButtonB = null;
                Points++;

                if (Points == mMaxPoints)
                {
                    // sieg anzeigen
                    mTimer.Stop();

                    Label lblWin = new()
                    {
                        Content = "Sie haben gewonnen!",
                        FontSize = 40,
                        Foreground = Brushes.Red,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        Background = new SolidColorBrush(Color.FromArgb(0xaa, 0xaa, 0xaa, 0xaa))
                    };
                    Grid.SetColumnSpan(lblWin, grdField.ColumnDefinitions.Count);
                    Grid.SetRowSpan(lblWin, grdField.RowDefinitions.Count);
                    _ = grdField.Children.Add(lblWin);
                }
            }
        }

        private void CommandNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = lvImageSets is not null &&
                lvImageSets.SelectedItems.Count > 0 &&
                HorizontalSize > 0 &&
                VerticalSize > 0 &&
                HorizontalSize * VerticalSize % 2 == 0;
        }

        private void CommandNew_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string[] folderList = new string[lvImageSets.SelectedItems.Count];
            for (int counter = 0; counter < folderList.Length; counter++)
                folderList[counter] = lvImageSets.SelectedItems[counter].ToString();
            resetGame(HorizontalSize, VerticalSize, folderList); // startet den Reset-Vorgang anhand der mitgelieferten grösse.
        }

        private void CommandHelp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _ = MessageBox.Show("Platzhalter für hilfe");
        }

        private void CommandPlay_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mMusic.Play();
        }

        private void CommandNextTrack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mMusic.Stop();
        }

        private void CommandPreviousTrack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mMusic.Stop();
        }

        private void CommandStop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mMusic.Stop();
        }
    }
}

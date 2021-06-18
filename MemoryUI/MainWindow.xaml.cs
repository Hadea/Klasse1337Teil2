using System;
using System.Collections.Generic;
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
using System.Windows.Threading;
using System.Reflection;
using System.IO;

namespace MemoryUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Button mSelectedButtonA; // erste auswahl
        private Button mSelectedButtonB; // zweite auswahl für vergleich
        private DateTime mTimeGameStart;


        public DispatcherTimer Timer
        {
            get
            {
                if (mTimer is null)
                {
                    mTimer = new DispatcherTimer(
                        new TimeSpan(0, 0, 0, 0, 100),
                        DispatcherPriority.Render,
                        (_, _) => lblTime.Text = $"Zeit: {(DateTime.Now - mTimeGameStart).TotalSeconds.ToString("N1")}",
                        Dispatcher.CurrentDispatcher);
                }
                return mTimer;
            }
        }
        private DispatcherTimer mTimer;


        public int Points
        {
            get => mPoints;
            set
            {
                mPoints = value;
                tblPoints.Text = "Punkte: " + mPoints;
            }
        }
        private int mPoints;

        public int Turns
        {
            get => mTurns;
            set
            {
                mTurns = value;
                tblTurns.Text = "Züge: " + mTurns;
            }
        }
        private int mTurns;
        private List<BitmapImage> bitmaps;

        public MainWindow()
        {
            InitializeComponent();
            loadAllBitmapimages();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(tbxFieldsHorizontal.Text, out int horizontal)) return;
            if (!int.TryParse(tbxFieldsVertical.Text, out int vertical)) return;
            resetGame(horizontal, vertical);
        }

        private void loadAllBitmapimages()
        {
            bitmaps = new List<BitmapImage>();
            foreach (string fileName in Directory.GetFiles("Images"))
            {
                BitmapImage tempBitmap = new(); // neues Bild erstellen
                tempBitmap.BeginInit();// füllen des Bildes starten
                tempBitmap.UriSource = new Uri(Directory.GetParent(Environment.CommandLine).FullName + @"\" + fileName);// bildinhalt aus datei laden
                tempBitmap.EndInit();// füllen des Bildes finalisieren
                bitmaps.Add(tempBitmap);
            }
        }
        private void resetGame(int Columns, int Rows)
        {
            if (Columns * Rows % 2 == 1) throw new ArgumentOutOfRangeException();

            mTimeGameStart = DateTime.Now;

            // spielfeld leeren
            grdField.Children.Clear();
            grdField.RowDefinitions.Clear();
            grdField.ColumnDefinitions.Clear();

            GridLength cellSize = new(100);

            // rows hinzufügen
            for (int counter = 0; counter < Rows; counter++)
            {
                RowDefinition newRow = new()
                {
                    Height = cellSize
                };
                grdField.RowDefinitions.Add(newRow);
            }
            // cols hinzufügen

            for (int counter = 0; counter < Columns; ++counter)
            {
                ColumnDefinition newCol = new()
                {
                    Width = cellSize
                };
                grdField.ColumnDefinitions.Add(newCol);
            }

            // liste der bilder vorbereiten aus welcher ein element jedem button zugewiesen ist
            // die liste enthält bereits ein paar von jedem bild

            List<BitmapImage> imageListWithPairs = new();
            //TODO hier aufgehört

                // eigene assembly heraussuchen

            for (int counter = 0; counter < bitmaps.Count && counter < Rows * Columns / 2; counter++)
            {
                imageListWithPairs.Add(bitmaps[counter]);
                imageListWithPairs.Add(bitmaps[counter]);
            }

            Random rndGen = new();

            // alle zellen durchgehen
            for (int rowCounter = 0; rowCounter < Rows; rowCounter++)
            {
                for (int colCounter = 0; colCounter < Columns; colCounter++)
                {
                    //      button erstellen
                    Button newButton = new();
                    newButton.Content = "Test " + (rowCounter * Columns + colCounter);
                    //todo: verstecktes zufälliges bild zuweisen
                    int chosenImageID = rndGen.Next(imageListWithPairs.Count);
                    Uri imageLink = new(imageListWithPairs[chosenImageID], UriKind.Relative);
                    newButton.Content = new Image() { Source = new BitmapImage(imageLink) };
                    imageListWithPairs.RemoveAt(chosenImageID);

                    //Grid-Position des button einstellen
                    Grid.SetColumn(newButton, colCounter);
                    Grid.SetRow(newButton, rowCounter);
                    grdField.Children.Add(newButton);//      Button in den Grid-Children eintragen

                }
            }// ende alles durchgehen
        }

        private void btnField_Click(object sender, RoutedEventArgs e)
        {
            if (!Timer.IsEnabled)
            {
                mTimeGameStart = DateTime.Now;
                Timer.Start();
            }

            #region Zwei ungleiche aufgedeckt und einen dritten gewählt
            if (mSelectedButtonA is not null && mSelectedButtonB is not null)
            {
                // zwei unterschiedliche ausgewählt gewesen welche noch offen sind

                // bereits gewählte wieder verstecken
                (mSelectedButtonA.Content as Image).Visibility = Visibility.Collapsed;
                (mSelectedButtonB.Content as Image).Visibility = Visibility.Collapsed;

                // auswahl aufheben
                mSelectedButtonA = null;
                mSelectedButtonB = null;
                ((sender as Button).Content as Image).Visibility = Visibility.Visible;
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


            mSelectedButtonB = sender as Button;
            Turns++;

            // zweiter wird aufgedeckt und verglichen
            if ((mSelectedButtonA.Content as Image).Source != (mSelectedButtonB.Content as Image).Source) //todo: Vergleich Testen!
            {
                return;
            }

            mSelectedButtonA.IsEnabled = false;
            mSelectedButtonB.IsEnabled = false;
            mSelectedButtonA = null;
            mSelectedButtonB = null;
            Points++;
            Timer.Stop();
            // wenn erste auswahl leer
            //      gewählten button als erste auswahl speichern
            // andernfalls (erste befüllt, aber nicht die zweite)
            //      wenn inhalt beider button gleich
            //          beide button deaktivieren
            //          button aus auswahl entfernen
            //          punkte raufzählen
            //          wenn punkte gleich buttonanzahl halbiert
            //              Spielende
            //          ende wenn
            //      ende wenn
            // ende andernfalls
        }
    }
}

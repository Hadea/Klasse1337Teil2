using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MemoryDakkakEdition
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            reset();
        }

        /// <summary>
        /// Shuffles all buttons in the grid and collapses the Image inside the button
        /// </summary>
        private void reset()
        {
            // Eine liste mit allen möglichen Koordinaten erstellen (0,0) (0,1) (0,2) ...
            List<System.Drawing.Point> unusedCoordinates = new();
            for (int counterY = 0; counterY < grdCardField.RowDefinitions.Count; counterY++)
            {
                for (int counterX = 0; counterX < grdCardField.ColumnDefinitions.Count; counterX++)
                {
                    unusedCoordinates.Add(new(counterX, counterY));
                }
            }

            Random rndGen = new();
            // alle button durchgehen und die Position im Grid wechseln
            foreach (Button item in grdCardField.Children)
            {
                // zufälligen Eintrag aus der liste der unbenutzten koordinaten auswählen
                int choosenPointID = rndGen.Next(unusedCoordinates.Count);
                System.Drawing.Point choosenPoint = unusedCoordinates[choosenPointID];

                // gewählten eintrag löschen, damit er nicht zweimal gewählt wird
                unusedCoordinates.RemoveAt(choosenPointID);

                // aktuellen button auf die neue Position bringen
                Grid.SetRow(item, choosenPoint.Y);
                Grid.SetColumn(item, choosenPoint.X);

                // das Bild auf dem Button verstecken
                (item.Content as Image).Visibility = Visibility.Collapsed;
            }
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            reset();
        }

        // hilfsvariablen um festzuhalten welche Karten gerade offen gezeigt werden
        private int selectedFirstID;
        private int selectedSecondID;

        private void btnField_Click(object sender, RoutedEventArgs e)
        {
            // wenn bisher kein button geklickt
            if (selectedFirstID == -1)
            {
                selectedFirstID = grdCardField.Children.IndexOf(sender as Button);
                ((grdCardField.Children[selectedFirstID] as Button).Content as Image).Visibility = Visibility.Visible;
                return;
            }

            // wenn bisher nur ein button geklickt wurde
            if (selectedSecondID == -1)
            {
                int currentID = grdCardField.Children.IndexOf(sender as Button);
                if (currentID == selectedFirstID) return; // 2x gleicher button geklickt

                selectedSecondID = currentID;
                ((grdCardField.Children[selectedSecondID] as Button).Content as Image).Visibility = Visibility.Visible;

                // fieser test das sie aufsteigend sind und mit einer geraden zahl beginnen
                if (Math.Min(selectedFirstID, selectedSecondID) % 2 == 0 &&
                    Math.Min(selectedFirstID, selectedSecondID) + 1 == Math.Max(selectedFirstID, selectedSecondID))
                {
                    // paar gefunden
                    grdCardField.Children[selectedFirstID].IsEnabled = false;
                    grdCardField.Children[selectedSecondID].IsEnabled = false;
                    selectedFirstID = -1;
                    selectedSecondID = -1;
                }
            }
            else // bereits zwei karten offen
            {
                // offene verdecken und neue karte zeigen
                ((grdCardField.Children[selectedFirstID] as Button).Content as Image).Visibility = Visibility.Collapsed;
                ((grdCardField.Children[selectedSecondID] as Button).Content as Image).Visibility = Visibility.Collapsed;
                selectedFirstID = grdCardField.Children.IndexOf(sender as Button);
                ((grdCardField.Children[selectedFirstID] as Button).Content as Image).Visibility = Visibility.Visible;
                selectedSecondID = -1;
            }
        }
    }
}

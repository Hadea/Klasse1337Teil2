using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ComponentsDemo
{
    /// <summary>
    /// Interaction logic for AudioDemoPage.xaml
    /// </summary>
    public partial class AudioDemoPage : Page
    {
        private readonly MediaPlayer mMusic;
        private readonly SoundPlayer mSound;
        public AudioDemoPage()
        {
            InitializeComponent();
            // Inztanzieren des Mediaplayers
            mMusic = new();
            
            // Das Lied wird in dieser Demo nicht gewechselt sodass wir es direkt öffnen.
            // Fehlgeschlagenes öffnen (Datei nicht vorhanden, etc) führt nicht zu einer
            // Execption! Der Mediaplayer hat ein Event "MediaFailed" auf das reagiert
            // werden kann wenn eine Datei nicht ladbar ist.
            mMusic.Open(new Uri("Audio/Kraddy-DubStep.mp3", UriKind.Relative));

            // Das Event MediaEnded wird ausgelöst wenn das Lied bis zum Ende gespielt wurde.
            // Hier könnte man entweder ein neues Lied laden (Playlist) oder, wie hier, das
            // lied wiederholen lassen.
            mMusic.MediaEnded += loop;

            // der Soundplayer kann nur einen einzigen WAV auf einmal ausgeben, egal wieviele
            // Inztanzen der Klasse SoundPlayer existieren. Daher ist es nur für sehr kurze
            // effekte brauchbar. Kann auch als <SoundPlayerAction /> in XAML benutzt werden
            mSound = new("Audio/Klick.wav");
            
        }

        /// <summary>
        /// Event Handler für das MediaEnded Event des <see cref="MediaPlayer"/>.
        /// Startet das Lied von vorn.
        /// </summary>
        /// <param name="sender">unbenutzt</param>
        /// <param name="e">unbenutzt</param>
        private void loop(object sender, EventArgs e)
        {
            mMusic.Position = TimeSpan.Zero;
            mMusic.Play();
        }

        /// <summary>
        /// Event Handler um das geladene Lied abzuspielen
        /// </summary>
        /// <param name="sender">unbenutzt</param>
        /// <param name="e">unbenutzt</param>
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            mMusic.Play();
        }

        /// <summary>
        /// Event Handler um das geladene Lied zu pausieren
        /// </summary>
        /// <param name="sender">unbenutzt</param>
        /// <param name="e">unbenutzt</param>
        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mMusic.Pause();
        }

        /// <summary>
        /// Event Handler um das geladene Lied zu stoppen
        /// </summary>
        /// <param name="sender">unbenutzt</param>
        /// <param name="e">unbenutzt</param>
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mMusic.Position = TimeSpan.Zero;
            mMusic.Pause();
        }

        /// <summary>
        /// Event Handler um das geladene Lied ab der aktuellen position weiterzuspielen
        /// </summary>
        /// <param name="sender">unbenutzt</param>
        /// <param name="e">unbenutzt</param>
        private void btnSoundPlayer_Click(object sender, RoutedEventArgs e) => mSound.Play();

        #region Systemsounds
        // Event Handler für SystemSounds
        // Diese nutzen die im Betriebssystem eingestellten Standard-Sounds und benötigen deshalb keine
        // Inztanz eines Players

        private void btnSystemA_Click(object sender, RoutedEventArgs e) => SystemSounds.Exclamation.Play();
        private void btnSystemB_Click(object sender, RoutedEventArgs e) => SystemSounds.Asterisk.Play();
        private void btnSystemC_Click(object sender, RoutedEventArgs e) => SystemSounds.Question.Play();
        private void btnSystemD_Click(object sender, RoutedEventArgs e) => SystemSounds.Beep.Play();
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
    /// Interaction logic for AudioDemoPage.xaml
    /// </summary>
    public partial class AudioDemoPage : Page
    {
        private readonly MediaPlayer mMusic;
        private readonly SoundPlayer mSound;
        public AudioDemoPage()
        {
            InitializeComponent();
            mMusic = new();
            mMusic.Open(new Uri("Audio/Kraddy-DubStep.mp3", UriKind.Relative));
            mMusic.MediaEnded += loop;
            mSound = new("Audio/Klick.wav");
        }

        private void loop(object sender, EventArgs e)
        {
            mMusic.Position = TimeSpan.Zero;
            mMusic.Play();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            mMusic.Play();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            mMusic.Pause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            mMusic.Position = TimeSpan.Zero;
            mMusic.Pause();
        }

        private void btnSoundPlayer_Click(object sender, RoutedEventArgs e)
        {
            mSound.Play();
        }

        private void btnSystemA_Click(object sender, RoutedEventArgs e)
        {
            SystemSounds.Exclamation.Play();
        }

        private void btnSystemB_Click(object sender, RoutedEventArgs e)
        {
            SystemSounds.Asterisk.Play();
        }

        private void btnSystemC_Click(object sender, RoutedEventArgs e)
        {
            SystemSounds.Question.Play();
        }
        private void btnSystemD_Click(object sender, RoutedEventArgs e)
        {
            SystemSounds.Beep.Play();
        }
    }
}

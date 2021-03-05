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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pr0j3ct0r.User_Interface
{
    /// <summary>
    /// Interaction logic for VideoPlayer.xaml
    /// </summary>
    public partial class VideoPlayer : Window
    {
        public VideoPlayer()
        {
            InitializeComponent();
        }

        // A timer to display the video's location.
        private DispatcherTimer timerVideoTime;

        // Create the timer and otherwise get ready.
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timerVideoTime = new DispatcherTimer();
            timerVideoTime.Interval = TimeSpan.FromSeconds(0.1);
            timerVideoTime.Tick += new EventHandler(timer_Tick);
            videoPlayer.Stop();
        }

        private void videoPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            sbarPosition.Minimum = 0;
            sbarPosition.Maximum = videoPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            sbarPosition.Visibility = Visibility.Visible;
        }

        // Show the play position in the ScrollBar and TextBox.
        private void ShowPosition()
        {
            sbarPosition.Value = videoPlayer.Position.TotalSeconds;
            txtPosition.Text = videoPlayer.Position.TotalSeconds.ToString("0.0");
        }

        // Enable and disable appropriate buttons.
        private void EnableButtons(bool is_playing)
        {
            if (is_playing)
            {
                btnPlay.IsEnabled = false;
                btnPause.IsEnabled = true;
                btnPlay.Opacity = 0.5;
                btnPause.Opacity = 1.0;
            }
            else
            {
                btnPlay.IsEnabled = true;
                btnPause.IsEnabled = false;
                btnPlay.Opacity = 1.0;
                btnPause.Opacity = 0.5;
            }
            timerVideoTime.IsEnabled = is_playing;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            videoPlayer.Play();
            EnableButtons(true);
        }
        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            videoPlayer.Pause();
            EnableButtons(false);
        }
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            videoPlayer.Stop();
            EnableButtons(false);
            ShowPosition();
        }
        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            videoPlayer.Stop();
            videoPlayer.Play();
            EnableButtons(true);
        }
        private void btnFaster_Click(object sender, RoutedEventArgs e)
        {
            videoPlayer.SpeedRatio *= 1.5;
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            videoPlayer.Position += TimeSpan.FromSeconds(10);
            ShowPosition();
        }
        private void btnSlower_Click(object sender, RoutedEventArgs e)
        {
            videoPlayer.SpeedRatio /= 1.5;
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            videoPlayer.Position -= TimeSpan.FromSeconds(10);
            ShowPosition();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            ShowPosition();
        }

        private void btnSetPosition_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan timespan = TimeSpan.FromSeconds(double.Parse(txtPosition.Text));
            videoPlayer.Position = timespan;
            ShowPosition();
        }
    }
}

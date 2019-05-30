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
using System.Windows.Media;
using Microsoft.Win32;
using System.Windows.Threading;
using System.Threading;

namespace ThreadHomeWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer mediaPlayer;
        Thread daemonMusic;
        public MainWindow()
        {
            InitializeComponent();
            mediaPlayer = new MediaPlayer();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                mediaPlayer.Open(new Uri(openFileDialog.FileName));

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void playMusicButtonClick(object sender, RoutedEventArgs e)
        {
            daemonMusic = new Thread(PlayMusic);
            daemonMusic.IsBackground = true;
            daemonMusic.Start();
        }

        private void PlayMusic()
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate { mediaPlayer.Play(); }));
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer.Source != null)
                musicLabel.Content = mediaPlayer.Position.ToString(@"mm\:ss");
            else
                musicLabel.Content = "No file selected...";
        }
    }
}

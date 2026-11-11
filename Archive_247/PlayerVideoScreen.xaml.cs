using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Archive_247
{
    public partial class PlayerVideoScreen : UserControl
    {
        private string _currentCamName;
        private bool _isDangerCamera = false;

        private bool _isLocked;

        public PlayerVideoScreen(string camName, bool isLocked = true)
        {
            InitializeComponent();
            _isLocked = isLocked;
            PlayVideo(camName);
            UpdateButtonsState();
        }

        private void UpdateButtonsState()
        {
            var allButtons = new[] { Btn01, Btn02, Btn03, Btn04, Btn05, Btn06, Btn07, Btn08, Btn09 };

            foreach (var btn in allButtons)
            {
                string camName = btn.Tag.ToString();
                if (DestroyedCamerasManager.IsCameraDestroyed(camName))
                {
                    btn.IsEnabled = false;
                    btn.Opacity = 0.4;
                }
                else
                {
                    btn.IsEnabled = true;
                    btn.Opacity = 1.0;
                }
            }
        }

        private void PlayVideo(string camName)
        {
            _currentCamName = camName;
            SelectedCamHeader.Text = camName;
            FloorHeader.Text = GetFloorByCamera(camName);

            _isDangerCamera = (camName == "CAM 01" || camName == "CAM 02" || camName == "CAM 05");

            SetCamerasInteractivity(!_isLocked);

            HighlightActiveButton(camName);

            string cleanCamName = camName.Replace(" ", "_");
            string videoPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Videos", $"{cleanCamName}.mp4");

            if (File.Exists(videoPath))
            {
                VideoPlayer.Source = new Uri(videoPath);
                VideoPlayer.Play();
            }
            else
            {
                SelectedCamHeader.Text = $"{camName} (NO VIDEO)";
                VideoPlayer.Stop();
            }
        }

        private void SetCamerasInteractivity(bool isEnabled)
        {
            Btn01.IsHitTestVisible = isEnabled;
            Btn02.IsHitTestVisible = isEnabled;
            Btn03.IsHitTestVisible = isEnabled;
            Btn04.IsHitTestVisible = isEnabled;
            Btn05.IsHitTestVisible = isEnabled;
            Btn06.IsHitTestVisible = isEnabled;
            Btn07.IsHitTestVisible = isEnabled;
            Btn08.IsHitTestVisible = isEnabled;
            Btn09.IsHitTestVisible = isEnabled;
        }

        private void VideoPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (_isDangerCamera && !DestroyedCamerasManager.IsCameraDestroyed(_currentCamName))
            {
                DestroyedCamerasManager.DestroyCamera(_currentCamName);

                VideoPlayer.Stop();

                MessageBox.Show("КРИТИЧЕСКАЯ ОШИБКА: Файл поврежден.\nЗапись самоуничтожена и недоступна для дальнейшего просмотра.",
                                "СИСТЕМА БЕЗОПАСНОСТИ",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

                (Window.GetWindow(this) as MainWindow)?.ShowCameraMapScreen();
            }
            else if (_currentCamName == "CAM 07" || _currentCamName == "CAM 08" || _currentCamName == "CAM 09" || _currentCamName == "CAM 04" || _currentCamName == "CAM 03" || _currentCamName == "CAM 06")
            {
                VideoPlayer.Stop();
                (Window.GetWindow(this) as MainWindow)?.ShowCameraMapScreen();
            }
            else
            {
                VideoPlayer.Position = TimeSpan.Zero;
                VideoPlayer.Play();
            }
        }

        private string GetFloorByCamera(string camName)
        {
            switch (camName)
            {
                case "CAM 01":
                case "CAM 02":
                case "CAM 07":
                    return "2 ЭТАЖ";

                case "CAM 05":
                    return "ЛЕСТНИЦА";

                case "CAM 03":
                case "CAM 04":
                case "CAM 06":
                    return "3 ЭТАЖ";

                case "CAM 08":
                case "CAM 09":
                    return "1 ЭТАЖ";

                default:
                    return "НЕИЗВЕСТНО";
            }
        }

        private void HighlightActiveButton(string activeCamName)
        {
            Btn01.Background = (Brush)Application.Current.Resources["BtnBgColor"];
            Btn01.Foreground = (Brush)Application.Current.Resources["TextColor"];
            Btn02.Background = (Brush)Application.Current.Resources["BtnBgColor"];
            Btn02.Foreground = (Brush)Application.Current.Resources["TextColor"];
            Btn03.Background = (Brush)Application.Current.Resources["BtnBgColor"];
            Btn03.Foreground = (Brush)Application.Current.Resources["TextColor"];
            Btn04.Background = (Brush)Application.Current.Resources["BtnBgColor"];
            Btn04.Foreground = (Brush)Application.Current.Resources["TextColor"];
            Btn05.Background = (Brush)Application.Current.Resources["BtnBgColor"];
            Btn05.Foreground = (Brush)Application.Current.Resources["TextColor"];
            Btn06.Background = (Brush)Application.Current.Resources["BtnBgColor"];
            Btn06.Foreground = (Brush)Application.Current.Resources["TextColor"];
            Btn07.Background = (Brush)Application.Current.Resources["BtnBgColor"];
            Btn07.Foreground = (Brush)Application.Current.Resources["TextColor"];
            Btn08.Background = (Brush)Application.Current.Resources["BtnBgColor"];
            Btn08.Foreground = (Brush)Application.Current.Resources["TextColor"];
            Btn09.Background = (Brush)Application.Current.Resources["BtnBgColor"];
            Btn09.Foreground = (Brush)Application.Current.Resources["TextColor"];

            switch (activeCamName)
            {
                case "CAM 01": Btn01.Background = Brushes.White; Btn01.Foreground = Brushes.DarkGray; break;
                case "CAM 02": Btn02.Background = Brushes.White; Btn02.Foreground = Brushes.DarkGray; break;
                case "CAM 03": Btn03.Background = Brushes.White; Btn03.Foreground = Brushes.DarkGray; break;
                case "CAM 04": Btn04.Background = Brushes.White; Btn04.Foreground = Brushes.DarkGray; break;
                case "CAM 05": Btn05.Background = Brushes.White; Btn05.Foreground = Brushes.DarkGray; break;
                case "CAM 06": Btn06.Background = Brushes.White; Btn06.Foreground = Brushes.DarkGray; break;
                case "CAM 07": Btn07.Background = Brushes.White; Btn07.Foreground = Brushes.DarkGray; break;
                case "CAM 08": Btn08.Background = Brushes.White; Btn08.Foreground = Brushes.DarkGray; break;
                case "CAM 09": Btn09.Background = Brushes.White; Btn09.Foreground = Brushes.DarkGray; break;
            }
        }

        private void CameraButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isLocked) return;

            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                string camName = clickedButton.Tag.ToString();

                if (DestroyedCamerasManager.IsCameraDestroyed(camName)) return;

                PlayVideo(camName);
            }
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            VideoPlayer.Stop();
            (Window.GetWindow(this) as MainWindow)?.ShowCameraMapScreen();

        }
    }
}
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Archive_247
{
    public partial class MainWindow : Window
    {
        private SoundPlayer _soundPlayer;
        public MainWindow()
        {
            InitializeComponent();

            ShowLoginScreen();

            Loaded += (s, e) =>
            {
                string soundPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sounds", "shum.mp3");

                BackgroundNoise.Source = new Uri(soundPath, UriKind.Absolute);
                BackgroundNoise.Play();
            };

            _soundPlayer = new SoundPlayer(".sounds/console_hover.wav");
        }

        public void PlayUiSound(string fileName)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                try
                {
                    string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sounds", fileName);
                    if (!System.IO.File.Exists(path)) return;

                    MediaElement tempPlayer = new MediaElement
                    {
                        LoadedBehavior = MediaState.Manual,
                        UnloadedBehavior = MediaState.Stop,
                        Volume = 0.6,
                        Source = new Uri(path, UriKind.Absolute),
                        Visibility = Visibility.Collapsed,
                        Width = 0,
                        Height = 0
                    };

                    MainContentGrid.Children.Add(tempPlayer);

                    tempPlayer.MediaEnded += (s, e) =>
                    {
                        tempPlayer.Stop();
                        MainContentGrid.Children.Remove(tempPlayer);
                    };

                    tempPlayer.Play();
                }
                catch { }
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        private void BackgroundNoise_MediaEnded(object sender, RoutedEventArgs e)
        {
            BackgroundNoise.Position = System.TimeSpan.Zero;
            BackgroundNoise.Play();
        }

        public void ShowLoginScreen()
        {
            MainContentGrid.Children.Clear();
            LoginScreen screen = new LoginScreen();
            MainContentGrid.Children.Add(screen);

            screen.EnterPasswordBtn.MouseEnter += (s, e) => PlayUiSound("console_hover.mp3");
            screen.EnterPasswordBtn.Click += (s, e) => PlayUiSound("console_click.mp3");

            screen.ExitBtn.MouseEnter += (s, e) => PlayUiSound("console_hover.mp3");
            screen.ExitBtn.Click += (s, e) => PlayUiSound("console_click.mp3");
        }

        public void ShowPasswordScreen()
        {
            MainContentGrid.Children.Clear();
            PasswordScreen screen = new PasswordScreen();
            MainContentGrid.Children.Add(screen);

            screen.BackBtn.MouseEnter += (s, e) => PlayUiSound("console_hover.mp3");
            screen.BackBtn.Click += (s, e) => PlayUiSound("console_click.mp3");
        }

        public void ShowArchiveScreen()
        {
            MainContentGrid.Children.Clear();
            ArchiveMenuScreen screen = new ArchiveMenuScreen();
            MainContentGrid.Children.Add(screen);

            screen.CameraSystemBtn.MouseEnter += (s, e) => PlayUiSound("console_hover.mp3");
            screen.CameraSystemBtn.Click += (s, e) => PlayUiSound("console_click.mp3");

            screen.DeletedBtn.MouseEnter += (s, e) => PlayUiSound("console_hover.mp3");
            screen.DeletedBtn.Click += (s, e) => PlayUiSound("console_click.mp3");

            screen.ExitBtn.MouseEnter += (s, e) => PlayUiSound("console_hover.mp3");
            screen.ExitBtn.Click += (s, e) => PlayUiSound("console_click.mp3");
        }


        public void ShowCameraMapScreen()
        {
            MainContentGrid.Children.Clear();
            CameraMapScreen screen = new CameraMapScreen();
            MainContentGrid.Children.Add(screen);

            var cameraButtons = new[] { screen.Btn01, screen.Btn02, screen.Btn03, screen.Btn04, screen.Btn05, screen.Btn06, screen.Btn07, screen.Btn08, screen.Btn09 };

            foreach (var btn in cameraButtons)
            {
                btn.MouseEnter += (s, e) => PlayUiSound("camera_hover.mp3");
                btn.Click += (s, e) => PlayUiSound("camera_click.mp3");
            }

            screen.ExitBtn.MouseEnter += (s, e) => PlayUiSound("console_hover.mp3");
            screen.ExitBtn.Click += (s, e) => PlayUiSound("console_click.mp3");
        }

        public void ShowVideoPlayerScreen(string camName, bool isLocked)
        {
            MainContentGrid.Children.Clear();
            PlayerVideoScreen screen = new PlayerVideoScreen(camName, isLocked);
            MainContentGrid.Children.Add(screen);

            var cameraButtons = new[] { screen.Btn01, screen.Btn02, screen.Btn03, screen.Btn04, screen.Btn05, screen.Btn06, screen.Btn07, screen.Btn08, screen.Btn09 };
            foreach (var btn in cameraButtons)
            {
                btn.MouseEnter += (s, e) => PlayUiSound("camera_hover.mp3");
                btn.Click += (s, e) => PlayUiSound("camera_click.mp3");
            }

            screen.BackBtn.MouseEnter += (s, e) => PlayUiSound("console_hover.mp3");
            screen.BackBtn.Click += (s, e) => PlayUiSound("console_click.mp3");
        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Application.Current.Shutdown();
            }
        }

        public void ShowAdminPanelScreen()
        {
            MainContentGrid.Children.Clear();
            AdminPanelScreen screen = new AdminPanelScreen();
            MainContentGrid.Children.Add(screen);

            screen.BtnExit.MouseEnter += (s, e) => PlayUiSound("console_hover.mp3");
        }


    }
}
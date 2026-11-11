using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Archive_247
{
    public partial class AdminPanelScreen : UserControl
    {
        private const string ADMIN_PASSWORD = "002";

        public AdminPanelScreen()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AdminPasswordBox.Password == ADMIN_PASSWORD)
            {
                AuthPanel.Visibility = Visibility.Collapsed;
                ErrorMsg.Visibility = Visibility.Collapsed;
                ControlPanel.Visibility = Visibility.Visible;

                LoadDestroyedCameras();
            }
            else
            {
                ErrorMsg.Visibility = Visibility.Visible;
            }
        }

        private void LoadDestroyedCameras()
        {
            RestoreButtonsPanel.Children.Clear();

            if (DestroyedCamerasManager.DestroyedCameras.Count == 0)
            {
                TextBlock noCams = new TextBlock { Text = "НЕТ УДАЛЕННЫХ КАМЕР.", FontFamily = (FontFamily)Application.Current.Resources["TerminalFont"], Foreground = System.Windows.Media.Brushes.Gray, FontSize = 16 };
                RestoreButtonsPanel.Children.Add(noCams);
                return;
            }

            foreach (string cam in DestroyedCamerasManager.DestroyedCameras)
            {
                Button btn = new Button
                {
                    Content = $"[ ВОССТАНОВИТЬ {cam} ]",
                    Style = (Style)Application.Current.Resources["TerminalLinkButton"],
                    Margin = new Thickness(0, 0, 20, 10)
                };

                btn.Click += (s, e) =>
                {
                    DestroyedCamerasManager.RestoreCamera(cam);
                    LoadDestroyedCameras();
                };

                RestoreButtonsPanel.Children.Add(btn);
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow)?.ShowArchiveScreen();
        }

        private void DeletedBtn_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow)?.ShowAdminPanelScreen();
        }
    }
}
using System;
using System.Collections.Generic;
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
    public partial class CameraMapScreen : UserControl
    {
        public CameraMapScreen()
        {
            InitializeComponent();
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

        private void CameraButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                string camName = clickedButton.Tag.ToString();

                if (DestroyedCamerasManager.IsCameraDestroyed(camName)) return;

                bool isDangerCamera = (camName == "CAM 01" || camName == "CAM 02" || camName == "CAM 05");

                if (isDangerCamera)
                {
                    var result = MessageBox.Show(
                        "ВНИМАНИЕ!\n\nФайл записи с этой камеры поврежден.\nПросмотр может привести к самоуничтожению данных.\n\nНажмите ОК, чтобы продолжить.",
                        "СИСТЕМА БЕЗОПАСНОСТИ",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.OK)
                    {
                        (Window.GetWindow(this) as MainWindow)?.ShowVideoPlayerScreen(camName, isLocked: true);
                    }
                }
                else
                {
                    (Window.GetWindow(this) as MainWindow)?.ShowVideoPlayerScreen(camName, isLocked: false);
                }
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow)?.ShowArchiveScreen();
        }
    }
}

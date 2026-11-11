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
    public partial class ArchiveMenuScreen : UserControl
    {
        public ArchiveMenuScreen()
        {
            InitializeComponent();
        }
        private void CameraSystemBtn_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow)?.ShowCameraMapScreen();
        }
        private void DeletedBtn_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow)?.ShowAdminPanelScreen();
        }
        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow)?.ShowLoginScreen();
        }

    }
}

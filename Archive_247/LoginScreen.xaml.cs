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
    public partial class LoginScreen : UserControl
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void ConsoleButton_MouseEnter(object sender, MouseEventArgs e)
        {
            // Воспроизводим звук наведения
            (Window.GetWindow(this) as MainWindow)?.PlayUiSound("console_hover.mp3");
        }

        private void EnterPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow)?.ShowPasswordScreen();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            // Воспроизводим звук клика
            (Window.GetWindow(this) as MainWindow)?.PlayUiSound("console_click.mp3");

            // Ваша логика выхода
            Application.Current.Shutdown();
        }

    }
}

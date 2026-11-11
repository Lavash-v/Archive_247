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
using System.Windows.Threading;

namespace Archive_247
{
    public partial class PasswordScreen : UserControl
    {
        private DispatcherTimer _successTimer;

        public PasswordScreen()
        {
            InitializeComponent();
            _successTimer = new DispatcherTimer();
            Loaded += (s, e) => PasswordInput.Focus();
        }

        private async void PasswordInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PasswordInput.Text.Length == 6)
            {
                if (PasswordInput.Text == "220901")
                {
                    StatusMessage.Text = "SUCCESSFULL";
                    StatusMessage.Foreground = System.Windows.Media.Brushes.White;
                    StatusMessage.Visibility = Visibility.Visible;

                    PasswordInput.IsEnabled = false;

                    _successTimer = new DispatcherTimer { Interval = System.TimeSpan.FromSeconds(2) };
                    _successTimer.Tick += (s, args) =>
                    {
                        _successTimer.Stop();
                        (Window.GetWindow(this) as MainWindow)?.ShowArchiveScreen();
                    };
                    _successTimer.Start();
                }
                else
                {
                    StatusMessage.Text = "INCORRECT. TRY AGAIN";
                    StatusMessage.Visibility = Visibility.Visible;
                    await Task.Delay(1500);

                    PasswordInput.Text = "";
                }
            }
            else
            {
                StatusMessage.Visibility = Visibility.Collapsed;
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            (Window.GetWindow(this) as MainWindow)?.ShowLoginScreen();
        }
    }
}

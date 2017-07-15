using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using System.Windows.Input;

namespace SteamworkGUI
{
    /// <summary>
    /// Option.xaml 的交互逻辑
    /// </summary>
    public partial class Login 
    {
        public bool firstVcode = true;
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(account.Text) || string.IsNullOrEmpty(password.Password)) return;
            SetProgressing(true);
           
            MainWindow._instance.CMDinput("Login "+account.Text+" "+password.Password);
        }

        private void Vcode_confirm_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(vcode.Text)) return;
            SetProgressing(true);
            if (firstVcode)
            {
                MainWindow._instance.CMDinput(vcode.Text);
                firstVcode = false;
            }
            else
            {
                MainWindow._instance.CMDinput("set_steam_guard_code "+vcode.Text);
                MainWindow._instance.CMDinput("Login " + account.Text + " " + password.Password);
            }
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void vcode_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((e.Key) == Key.Enter)
            {
                Vcode_confirm_Click(new object(),new RoutedEventArgs());
            }
        }

        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key) == Key.Enter)
            {
                Login_Click(new object(), new RoutedEventArgs());
            }
        }

        public void SetProgressing(bool tf)
        {
            if (!tf)
            {
                progressing.Visibility = Visibility.Hidden;
            }
            else
            {
                progressing.Visibility = Visibility.Visible;
            }
        }



        public void ShowMyDiolog(string content, string title)
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "OK",
                ColorScheme = MetroDialogColorScheme.Inverted
            };

            MessageDialogResult result = this.ShowModalMessageExternal(title, content,
                MessageDialogStyle.Affirmative, mySettings);
        }
    }
}

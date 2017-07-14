using System.Windows;
using System.Windows.Input;

namespace SteamworkGUI
{
    /// <summary>
    /// Option.xaml 的交互逻辑
    /// </summary>
    public partial class Login 
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._instance.CMDinput("Login "+account.Text+" "+password.Password);
        }

        private void Vcode_confirm_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._instance.CMDinput(vcode.Text);
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

        
    }
}

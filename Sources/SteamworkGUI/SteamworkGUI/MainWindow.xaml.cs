using System.Windows;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
namespace SteamworkGUI
{

    public partial class MainWindow
    {
        public static MainWindow _instance;
        public Core cmd;

        public Login LoginWindow = new Login();
        public Output output_window = new Output();

        public enum Status
        {
            preparing,
            ready,
            none,
            noneGame,
            UploadPackage,
            UploadDLC,
            OpeningScript
        };
        public Status status;

        bool Loggedin;
        String UploadPath = "";

        public MainWindow()
        {
            status = Status.preparing;
            InitializeComponent();
            _instance = this;
            cmd = new Core();
            cmd.init();
        }

        #region StatusBar
        public void SetStatusBar(string s, int color)
        {
            StatusBartext.Text = s;
            var bc = new BrushConverter();
            switch (color)
            {
                case 0: { StatusBar.Background = (Brush)bc.ConvertFrom("#FF007ACC"); break; }
                case 1: { StatusBar.Background = (Brush)bc.ConvertFrom("#FFFF0000"); break; }
            }
        }
        #endregion

        #region Upload
        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            // Auto Generate vpf scripts

            //CMDinput("run_app_build ..\scripts\app_build_1000.vdf");
            
        }
        private void FilesDrop_Drop(object sender, DragEventArgs e)
        {
            string msg = "Drop";
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                msg = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                if (!Directory.Exists(msg)) return;
                filespath.Content = Path.GetFileNameWithoutExtension(msg);
                Fullpath.Content = msg;
                UploadPath = msg;

                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri(@"Images/Folder1.png", UriKind.RelativeOrAbsolute);
                bi.EndInit();
                FilesIcon.Source = bi;
                UploadButton.Background = new SolidColorBrush(Colors.Green);
            }
        }

        #endregion

        #region Output_window
        void Window_output(string s,bool input=false)
        {   
            output_window.output.Text +="["+ DateTime.Now.ToLongTimeString().ToString()+"]"+(input?">> ":"<< ")  + s + Environment.NewLine;
            output_window.output.ScrollToEnd();
        }

        private void Output_Click(object sender, RoutedEventArgs e)
        {
            output_window.Show();
        }
        #endregion

        #region UI_Events
        private void Option_Click(object sender, RoutedEventArgs e)
        {
            Option window = new Option();
            window.ShowDialog();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (Loggedin) return;
            if (status == Status.preparing) return;
            LoginWindow.Show();
        }

        private void ChangeGames_Click(object sender, RoutedEventArgs e)
        {
            ChooseAppid window = new ChooseAppid();
            window.ShowDialog();
        }
        #endregion

        #region CMD
        public void AnalyzeReturn(string s)
        {
            Window_output(s);
            switch (s)
            {
                case "Loading Steam API...OK.":
                    {
                        status = Status.ready;
                        Login_Click(new object(), new RoutedEventArgs());
                        SetStatusBar("Not Ready - (Not Logged In)", 1);
                        break;
                    }
                case "FAILED with result code 5":
                    {
                        MessageBox.Show("Invalid Password,Please try again.", "Invalid Password");
                        LoginWindow.SetProgressing(false);
                        
                        break;
                    }
                case "Waiting for user info...OK":
                    {
                        LoginWindow.Hide();
                        LoginButton.Content = "logged in";
                        LoginButton.Foreground = new SolidColorBrush(Colors.LightGreen);
                        Loggedin = true;
                        SetStatusBar("Ready - (Logged In)", 0);
                        preparing.Visibility = Visibility.Hidden;
                        break;
                    }
                case "Steam Guard code:Login Failure: Invalid Login Auth Code":
                    {
                        MessageBox.Show("Invalid Login Auth Code,Please try again.", "Invalid Login Auth Code");
                        LoginWindow.SetProgressing(false);
                        break;
                    }
                default:
                    {
                        if (!String.IsNullOrEmpty(s))
                        {
                            if (s.Contains("You can also enter this code at any time using 'set_steam_guard_code'"))
                            {
                                LoginWindow.VerifyCode.Visibility = Visibility.Visible;
                                LoginWindow.Password.Visibility = Visibility.Hidden;
                                LoginWindow.SetProgressing(false);
                                LoginWindow.firstVcode = true;
                            }
                            else if (s.Contains("Steam Guard code:Login Failure: Invalid Login Auth Code"))
                            {
                                MessageBox.Show("Invalid Login Auth Code,Please try again.", "Invalid Login Auth Code");
                                LoginWindow.SetProgressing(false);
                                LoginWindow.firstVcode = false;
                                break;
                                
                            }
                            else if(s.Contains("Rate Limit Exceeded"))
                            {
                                MessageBox.Show("Rate Limit Exceeded,Please try again later.", "Rate Limit Exceeded");
                                LoginWindow.SetProgressing(false);
                                break;
                            }
                        }
                       
                        break;
                    }
            }
        }

        public void CMDinput(string s)
        {
            cmd.cmd.StandardInput.WriteLine(s);
            Window_output(s, true);
        }

        #endregion

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            CMDinput("quit");
            cmd.cmd.Kill();
            Application.Current.Shutdown(-1);
        }
    }
}
using System.Windows;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using MahApps.Metro.Controls.Dialogs;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace SteamworkGUI
{

    public partial class MainWindow
    {
        public static MainWindow _instance;
        public Core cmd;
        public Login LoginWindow = new Login();
        public Output output_window = new Output();
        public string user_name;
        public enum Status
        {
            preparing,
            ready,
            noneGame,
            GameSetted,
            UploadPackage,
        };
        public Status status;
        bool Loggedin;
        String UploadPath = "";
        string UploadDescription="";
        public int Appid = 0;

        public MainWindow()
        {
            status = Status.preparing;
            InitializeComponent();
            
            SetStatusBar("preparing", 1);
            preparing.Visibility = Visibility.Visible;
            _instance = this;
            Save_Load.LoadData();
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
            window.Show();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (Loggedin) return;
            if (status == Status.preparing) return;
            LoginWindow.Show();
        }

        private void LaunchtoGitHub(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Asixa/Steamwork-GUI");
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
                        LoginWindow.ShowMyDiolog("Please try again.", "Invalid Password");
                        LoginWindow.SetProgressing(false);
                        
                        break;
                    }
                case "Waiting for user info...OK":
                    {
                        LoginWindow.Hide();
                        LoginButton.Content = user_name;
                        LoginButton.Foreground = new SolidColorBrush(Colors.LightGreen);
                        Loggedin = true;
                        SetStatusBar("Ready - (Logged In)", 0);
                        preparing.Visibility = Visibility.Hidden;
                        break;
                    }
            /*    case "Steam Guard code:Login Failure: Invalid Login Auth Code":
                    {
                        LoginWindow.ShowMyDiolog("Please try again.", "Invalid Login Auth Code");
                        LoginWindow.SetProgressing(false);
                        break;
                    }*/
                case "FAILED with result code 65":
                    {
                        LoginWindow.ShowMyDiolog("Please try again.", "Invalid Login Auth Code");
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
                                LoginWindow.ShowMyDiolog("Please try again.", "Invalid Login Auth Code");
                                LoginWindow.SetProgressing(false);
                                LoginWindow.firstVcode = false;
                                break;
                                
                            }
                            else if(s.Contains("Rate Limit Exceeded"))
                            {
                               
                                LoginWindow.ShowMyDiolog("Please try again later.", "Rate Limit Exceeded");
                                LoginWindow.SetProgressing(false);
                                break;
                            }
                            else if (status == Status.UploadPackage)
                            {
                                if(s.Contains("Building depot"))
                                {
                                    Upload_dialog_controllor.SetMessage("Building depot...");
                                }
                                else if(s.Contains("Building file mapping"))
                                {
                                    Upload_dialog_controllor.SetMessage("Building file mapping...");
                                }
                                else if (s.Contains("Scanning content"))
                                {
                                    Upload_dialog_controllor.SetMessage("Scanning content...");
                                }
                                else if (s.Contains("Uploading content"))
                                {
                                    Upload_dialog_controllor.SetMessage("Uploading content...");
                                }
                                else if (s.Contains("Successfully finished appID"))
                                {
                                    Upload_dialog_controllor.CloseAsync();
                                    show_Dialog("Finished", "Your game has been successfully uploaded to Steam", true, false);
                                    status = Status.GameSetted;
                                }
                                else if (s.Contains("Failure"))
                                {
                                    Upload_dialog_controllor.CloseAsync();
                                    show_Dialog("Failure", s, true, false);
                                    status = Status.GameSetted;
                                }
                            }
                        }
                       
                        break;
                    }
            }
        }

        public void CMDinput(string s)
        {
            cmd.cmd.StandardInput.WriteLine(s);
            if (!String.IsNullOrEmpty(LoginWindow.password.Password)&& s.Contains(LoginWindow.password.Password))
            {
               s= s.Replace(LoginWindow.password.Password, "*************");
            }
            Window_output(s, true);
        }

        #endregion
               
        #region Upload
        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            UploadButtonDown();
        }

        public async void UploadButtonDown()
        {

            if (Appid == 0)
            {
                show_Dialog("Appid错误", "请重新设置您的Appid", true, true); return;
            }
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Confirm",
                NegativeButtonText = "Cancel",
                ColorScheme = MetroDialogColorScheme.Inverted,
                AnimateHide = false
            };
            MessageDialogResult result = await this.ShowMessageAsync("Are you sure to start uploading?", "Appid:"+ Appid+Environment.NewLine+"You cannot not cancel during the uploading",
            MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result == MessageDialogResult.Affirmative)
            {
                var mySettings2 = new MetroDialogSettings()
                {
                    AffirmativeButtonText = "Confirm",
                    NegativeButtonText = "Cancel",
                    ColorScheme = MetroDialogColorScheme.Inverted,
                    AnimateHide = true,
                    AnimateShow = false
                };

                var Des_result = await this.ShowInputAsync("Desciption", "About this build", mySettings2);

                if (Des_result == null) return;

                UploadDescription = Des_result;
                status = Status.UploadPackage;
                ShowProgressDialog();
            }
            else
            {
                return;
            }
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
                UploadButton.IsEnabled = true;
            }
        }

        public ProgressDialogController Upload_dialog_controllor;
        private async void ShowProgressDialog()
        {
            var mySettings = new MetroDialogSettings()
            {
                NegativeButtonText = "Close now",
                AnimateShow = false,
                AnimateHide = false,
                ColorScheme = MetroDialogColorScheme.Inverted

            };

            Upload_dialog_controllor = await this.ShowProgressAsync(
               "Please wait...",
               "copying files...",
               settings: mySettings);

            Upload_dialog_controllor.SetIndeterminate();
            Upload_dialog_controllor.SetCancelable(false);

            ScriptGenerator _generator = new ScriptGenerator();
            _generator.Generate(Appid, UploadPath, UploadDescription);
        }
        #endregion

        #region Dialog
        public async void show_Dialog(string title, string message, bool _AnimateHide, bool _AnimateShow)
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Confirm",
                NegativeButtonText = "Cancel",
                ColorScheme = MetroDialogColorScheme.Inverted,
                AnimateHide = _AnimateHide,
                AnimateShow = _AnimateShow
            };

            MessageDialogResult result = await this.ShowMessageAsync(title, message,
            MessageDialogStyle.Affirmative, mySettings);

        }

        private bool _shutdown;
        private async void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (e.Cancel) return;
            e.Cancel = true;
            if (_shutdown) return;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Quit",
                NegativeButtonText = "Cancel",
                AnimateShow = true,
                AnimateHide = false,
                ColorScheme = MetroDialogColorScheme.Inverted

            };

            var result = await this.ShowMessageAsync("Quit application?",
                "Sure you want to quit application?",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);

            _shutdown = result == MessageDialogResult.Affirmative;

            if (_shutdown)
            {
                Save_Load.SaveData();
                CMDinput("quit");
                cmd.cmd.Kill();
                Application.Current.Shutdown();
            }
        }

        #endregion


        private void AppidInput_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            if (((NumericUpDown)sender).Value != null)
            {
                Appid = (int)((NumericUpDown)sender).Value;
            }

        }
    }
}

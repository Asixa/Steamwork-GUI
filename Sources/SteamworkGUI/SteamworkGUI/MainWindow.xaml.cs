using System.Collections.ObjectModel;
using System.Dynamic;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Windows.Threading;
using System.Windows.Media;

namespace SteamworkGUI
{

    public partial class MainWindow
    {
        public static MainWindow _instance;
        Login LoginWindow = new Login();
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
        public Core cmd;
        public Output output_window=new Output();
        bool Loggedin;
        public MainWindow()
        {
            status = Status.preparing;
            InitializeComponent();
            _instance = this;
            cmd = new Core();
            cmd.init();
        }

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
        public void AnalyzeReturn(string s)
        {
            Window_output(s);
            switch (s)
            {
                case "Loading Steam API...OK.": status = Status.ready; break;
                case "FAILED with result code 5":MessageBox.Show("Password Wrong"); break;
                case "Waiting for user info...OK":
                        LoginWindow.Hide();
                        LoginButton.Content="logged in";
                        LoginButton.Foreground= new SolidColorBrush(Colors.LightGreen);
                        Loggedin = true;
                        break;
                default:  break;
            }
        }

        void Window_output(string s)
        {
            output_window.output.Text += ">> " + s + Environment.NewLine;
            output_window.output.ScrollToEnd();
        }

        public void CMDinput(string s)
        {
            cmd.cmd.StandardInput.WriteLine(s);
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(cmd!=null)
            cmd.Clear();
        }

        private void FilesDrop_Drop(object sender, DragEventArgs e)
        {
                string msg = "Drop";
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    msg = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                    filespath.Content = msg;
                }
        }
        private void Output_Click(object sender, RoutedEventArgs e)
        {
            output_window.Show();
        }
    }
}
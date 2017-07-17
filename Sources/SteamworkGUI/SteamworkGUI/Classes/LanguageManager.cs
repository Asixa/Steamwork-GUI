using System;
using System.IO;
using System.Windows;

namespace SteamworkGUI
{
    class LanguageManager 
    {
        public static LanguageManager _instance;

        public LanguageManager() {
            _instance = this;
        }
        public void LoadLanguage(string pack)
        {
           // CultureInfo currentCultureInfo = CultureInfo.CurrentCulture;

            ResourceDictionary langRd = null;
              try
             {
            string path = "/Languages/" + pack + ".xaml";
            langRd =Application.LoadComponent(new Uri(@path,UriKind.Relative)) as ResourceDictionary;
            }
            catch{
                char[] InvaildChar = Path.GetInvalidFileNameChars();
                if (InvaildChar.Length>0)
                 {
                 //含有非法字符 \ / : * ? " < > | 等
                MessageBox. Show(@"Language pack cannot be loaded "+Environment.NewLine+ @" becasue he path contains illegal character" + Environment.NewLine + @"(\ / : * ? ！@ # ￥ %……&*（）)..."
                    + "\n" + new Uri(Environment.CurrentDirectory + "/Languages/" + pack + ".xaml", UriKind.Absolute).ToString()
                , "Error" , MessageBoxButton.OK, MessageBoxImage.Error);
      }
            }

            if (langRd != null)
            {
                if (MainWindow._instance.Resources.MergedDictionaries.Count > 0)
                {
                    MainWindow._instance.Resources.MergedDictionaries.Clear();
                }
                MainWindow._instance.Resources.MergedDictionaries.Add(langRd);

                if (MainWindow._instance.LoginWindow.Resources.MergedDictionaries.Count > 0)
                {
                    MainWindow._instance.LoginWindow.Resources.MergedDictionaries.Clear();
                }
                MainWindow._instance.LoginWindow.Resources.MergedDictionaries.Add(langRd);

                if (MainWindow._instance.LoginWindow.Resources.MergedDictionaries.Count > 0)
                {
                    MainWindow._instance.LoginWindow.Resources.MergedDictionaries.Clear();
                }
                MainWindow._instance.LoginWindow.Resources.MergedDictionaries.Add(langRd);

                if (MainWindow._instance.output_window.Resources.MergedDictionaries.Count > 0)
                {
                    MainWindow._instance.output_window.Resources.MergedDictionaries.Clear();
                }
                MainWindow._instance.output_window.Resources.MergedDictionaries.Add(langRd);

                if (MainWindow._instance.option_window.Resources.MergedDictionaries.Count > 0)
                {
                    MainWindow._instance.option_window.Resources.MergedDictionaries.Clear();
                }
                MainWindow._instance.option_window.Resources.MergedDictionaries.Add(langRd);

                if (MainWindow._instance.option_window.detail.Resources.MergedDictionaries.Count > 0)
                {
                    MainWindow._instance.option_window.detail.Resources.MergedDictionaries.Clear();
                }
                MainWindow._instance.option_window.detail.Resources.MergedDictionaries.Add(langRd);

            }
        }
    }
}

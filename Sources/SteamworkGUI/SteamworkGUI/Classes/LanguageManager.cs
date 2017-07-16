using System;
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
                langRd =Application.LoadComponent(new Uri(@"Languages\" + pack + ".xaml", UriKind.Relative)) as ResourceDictionary;
            }
            catch
            {
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

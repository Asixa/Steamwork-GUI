using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SteamworkGUI.Windows
{
    /// <summary>
    /// OptionDetail.xaml 的交互逻辑
    /// </summary>
    public partial class OptionDetail : UserControl
    {
        public OptionDetail()
        {
            InitializeComponent();
        }

        private void Link_Click(object sender, RoutedEventArgs e)
        {
            string url = "";
            switch(((Button)sender).ToolTip.ToString())
            {
                case "Github": { url = "https://github.com/Asixa/Steamwork-GUI";break; }
                case "Asixa": { url = "https://github.com/Asixa"; break; }
                case "Fangxm": { url = "https://github.com/Fangxm233"; break; }
            }
            System.Diagnostics.Process.Start(url);
        }


        private void English_Click(object sender, RoutedEventArgs e)//eng
        {
            string lang = "en-US";
            MainWindow._instance.LanguagePack = lang;
            LanguageManager._instance.LoadLanguage(MainWindow._instance.LanguagePack);
        }

        private void Chinese_Click(object sender, RoutedEventArgs e)//chs
        {

            string lang = "zh-CN";
            MainWindow._instance.LanguagePack = lang;
            LanguageManager._instance.LoadLanguage(MainWindow._instance.LanguagePack);
        }
    }
}

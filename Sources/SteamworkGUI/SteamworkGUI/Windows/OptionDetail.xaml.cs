using System;
using System.Collections.Generic;
using System.IO;
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
            DirectoryInfo TheFolder = new DirectoryInfo(Directory.GetCurrentDirectory()+"/Languages");
            //遍历文件夹

            //遍历文件
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                Button button = new Button();
                button.Content = System.IO.Path.GetFileNameWithoutExtension(NextFile.Name);
                button.Click += Language_pack_Click;
                button.Background = Engish.Background;
                button.Foreground = Engish.Foreground;
                button.Height = 30;

                LanguagePackList.Children.Add(button);
            }
      
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
        private void Language_pack_Click(object sender, RoutedEventArgs e)//eng
        {
            MainWindow._instance.LanguagePack = System.IO.Path.GetFileNameWithoutExtension(((Button)sender).Content.ToString());
            LanguageManager._instance.LoadLanguage(MainWindow._instance.LanguagePack);
        }


    }
}

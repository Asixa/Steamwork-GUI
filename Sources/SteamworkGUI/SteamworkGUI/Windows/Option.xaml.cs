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
using System.Windows.Shapes;

namespace SteamworkGUI
{
    /// <summary>
    /// Option.xaml 的交互逻辑
    /// </summary>
    public partial class Option 
    {
        public Option()
        {
            InitializeComponent();
        }

        private void Dev_Click(object sender, RoutedEventArgs e)
        {
            try { System.Diagnostics.Process.Start("https://github.com/asixa"); } catch { }
        }

        private void Source_Click(object sender, RoutedEventArgs e)
        {
            try { System.Diagnostics.Process.Start("https://github.com/Asixa/Steamwork-GUI/"); } catch { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)//eng
        {
            ResourceDictionary langRd = null;
            string lang = "en-US";

            try
            {
                langRd =
                    Application.LoadComponent(
                             new Uri(@"Languages\" + lang + ".xaml", UriKind.Relative))
                    as ResourceDictionary;
            }
            catch
            {
            }

            if (langRd != null)
            {
                if (this.Resources.MergedDictionaries.Count > 0)
                {
                    this.Resources.MergedDictionaries.Clear();
                }
                this.Resources.MergedDictionaries.Add(langRd);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//chs
        {
            ResourceDictionary langRd = null;
            string lang = "zh-CN";

            try
            {
                langRd =
                    Application.LoadComponent(
                             new Uri(@"Languages\" + lang + ".xaml", UriKind.Relative))
                    as ResourceDictionary;
            }
            catch
            {
            }

            if (langRd != null)
            {
                if (this.Resources.MergedDictionaries.Count > 0)
                {
                    this.Resources.MergedDictionaries.Clear();
                }
                this.Resources.MergedDictionaries.Add(langRd);
            }
        }
    }
}

namespace SteamworkGUI
{
    /// <summary>
    /// Output.xaml 的交互逻辑
    /// </summary>
    public partial class Output 
    {
        public Output()
        {
            InitializeComponent();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    
    }
}

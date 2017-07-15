using System;
using System.Diagnostics;
namespace SteamworkGUI
{
     public class Core
    {
        public static Core _instance;
        public Process cmd;

        public  Core()
        {
            _instance = this;
        }

        public void Clear()
        {
            if(cmd!=null)
            cmd.Kill();
        }

        public bool init()
        {
            ProcessStartInfo psi = new ProcessStartInfo();

            psi.RedirectStandardOutput = true;    
            psi.RedirectStandardInput = true;           
            psi.RedirectStandardError = true;
            psi.FileName = "Tools/Steamcmd/steamcmd.exe";               
            psi.UseShellExecute = false;                  
            psi.CreateNoWindow = true;

            cmd = Process.Start(psi);                 
            cmd.OutputDataReceived +=DataReceived;
            cmd.ErrorDataReceived += DataReceived;
            cmd.BeginErrorReadLine();
            cmd.BeginOutputReadLine();

            return true;
        }

        public void DataReceived(object sender, DataReceivedEventArgs e)
        {
            MainWindow._instance.Dispatcher.BeginInvoke(new Action(() =>
            {
                MainWindow._instance.AnalyzeReturn(e.Data);
            }));
        }
    }
}

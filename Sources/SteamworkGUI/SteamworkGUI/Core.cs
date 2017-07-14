using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Windows;

using System.Collections.ObjectModel;
using System.Dynamic;
using System.Windows.Controls;
using System.Windows.Threading;
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
            psi.FileName = "E:/Steamcmd/steamcmd.exe";               
            psi.UseShellExecute = false;                  
            psi.CreateNoWindow = true;
            cmd = Process.Start(psi);                 
            cmd.OutputDataReceived +=DataReceived;
            cmd.ErrorDataReceived += ErrorReceived;
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
        public void ErrorReceived(object sender, DataReceivedEventArgs e)
        {
            MainWindow._instance.Dispatcher.BeginInvoke(new Action(() =>
            {
                MainWindow._instance.AnalyzeReturn(e.Data);
            }));
        }

    }
}

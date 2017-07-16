using System;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace SteamworkGUI
{
    class ScriptGenerator
    {
        static FileStream write_filestream;
        static FileStream read_filestream;
        static StreamWriter stream_writer;
        static StreamReader stream_reader;

        public delegate void GenerateComplete();
        public event GenerateComplete CompleteEvent;
        string app_build_script_path;

        public ScriptGenerator()
        {
            CompleteEvent +=new GenerateComplete(Ready_to_upload);
        }

        public string Generate(int Appid, String game_path,String Description)
        {
            string path = Directory.GetCurrentDirectory();

            string filePath = path + @"\Tools\Steamcmd\Scripts\app_build_" + Appid + ".vpf";
            app_build_script_path = filePath;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            read_filestream = new FileStream(path + @"\Tools\Steamcmd\Scripts\Template\app_build_[].vpf", FileMode.Open);
            write_filestream = new FileStream(filePath, FileMode.CreateNew);
            stream_writer = new StreamWriter(write_filestream);
            stream_reader = new StreamReader(read_filestream);
            string app_build_script = stream_reader.ReadToEnd();
            app_build_script = app_build_script.Replace("[Appid]", Appid.ToString());
            app_build_script = app_build_script.Replace("[Appid1]", (Appid + 1).ToString());
            app_build_script = app_build_script.Replace("[Description]", Description);
            stream_writer.WriteLine(app_build_script);
            stream_writer.Close();
            stream_reader.Close();
            write_filestream.Close();
            read_filestream.Close();

            filePath = path + @"\Tools\Steamcmd\Scripts\depot_build_" + (Appid+1) + ".vdf";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            read_filestream = new FileStream(path + @"\Tools\Steamcmd\Scripts\Template\depot_build_[].vdf", FileMode.Open);
            write_filestream = new FileStream(filePath, FileMode.CreateNew);
            stream_writer = new StreamWriter(write_filestream);
            stream_reader = new StreamReader(read_filestream);
            string depot_build_script = stream_reader.ReadToEnd();
            depot_build_script = depot_build_script.Replace("[Appid]", (Appid+1).ToString());
            depot_build_script = depot_build_script.Replace("[P_Game_path]", Directory.GetParent(game_path).FullName+@"\"); 
            depot_build_script = depot_build_script.Replace("[Game_path]", ".\\"+game_path + @"\*");
            stream_writer.WriteLine(depot_build_script);
            stream_writer.Close();
            stream_reader.Close();
            write_filestream.Close();
            read_filestream.Close();

            CompleteEvent.Invoke();
            return app_build_script_path;
        }

        public void Ready_to_upload()
        {
            MainWindow._instance.Dispatcher.BeginInvoke(new Action(() =>
            {
                MainWindow._instance.CMDinput("run_app_build "+app_build_script_path);
            }));
        }
    }
}

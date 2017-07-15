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

        public delegate void CopyComplete();
        public static event CopyComplete CopyCompleteEvent;

        public static string Generate(int Appid, String game_path)
        {
            string path = Directory.GetCurrentDirectory();
            
            if (Directory.Exists(path + @"\Tools\steamcmd\content")) 
            Directory.Delete(path + @"\Tools\steamcmd\content");

            Task.Run(() =>
            {
                CopyDir(game_path, path + @"\Tools\steamcmd\content\" + Path.GetFileNameWithoutExtension(game_path));
                CopyCompleteEvent.Invoke();    //复制完后会触发调用这个事件
            });
            
            string filePath = path + @"\Scripts\app_build_" + Appid + ".vpf";
            string app_build_script_path = filePath;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            read_filestream = new FileStream(path + @"\Scripts\Template\app_build_[].vpf", FileMode.Open);
            write_filestream = new FileStream(filePath, FileMode.CreateNew);
            stream_writer = new StreamWriter(write_filestream);
            stream_reader = new StreamReader(read_filestream);
            string app_build_script = stream_reader.ReadToEnd();
            app_build_script = app_build_script.Replace("[Appid]", Appid.ToString());
            app_build_script = app_build_script.Replace("[Appid1]", (Appid + 1).ToString());
            stream_writer.WriteLine(app_build_script);
            stream_writer.Close();
            stream_reader.Close();
            write_filestream.Close();
            read_filestream.Close();

            filePath = path + @"\Scripts\depot_build_" + Appid + ".vpf";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            read_filestream = new FileStream(path + @"\Scripts\Template\depot_build_[].vdf", FileMode.Open);
            write_filestream = new FileStream(filePath, FileMode.CreateNew);
            stream_writer = new StreamWriter(write_filestream);
            stream_reader = new StreamReader(read_filestream);
            string depot_build_script = stream_reader.ReadToEnd();
            depot_build_script = depot_build_script.Replace("[Appid]", Appid.ToString());
            //depot_build_script = depot_build_script.Replace("[P_Game_path]", Directory.GetParent(game_path).FullName); @"\Scripts\Template\depot_build_[].vdf"
            depot_build_script = depot_build_script.Replace("[P_Game_path]", @"\Tools\steamcmd\content");
            depot_build_script = depot_build_script.Replace("[Game_path]", "\\"+Path.GetFileNameWithoutExtension(game_path) + @"\*");
            stream_writer.WriteLine(depot_build_script);
            stream_writer.Close();
            stream_reader.Close();
            write_filestream.Close();
            read_filestream.Close();
            return app_build_script_path;
        }

        public static void CopyDir(string fromDir, string toDir)
        {
            if (!Directory.Exists(fromDir))
                return;

            if (!Directory.Exists(toDir))
            {
                Directory.CreateDirectory(toDir);
            }

            string[] files = Directory.GetFiles(fromDir);
            foreach (string formFileName in files)
            {
                string fileName = Path.GetFileName(formFileName);
                string toFileName = Path.Combine(toDir, fileName);
                File.Copy(formFileName, toFileName);
            }
            string[] fromDirs = Directory.GetDirectories(fromDir);
            foreach (string fromDirName in fromDirs)
            {
                string dirName = Path.GetFileName(fromDirName);
                string toDirName = Path.Combine(toDir, dirName);
                CopyDir(fromDirName, toDirName);
            }
        }

        public static void MoveDir(string fromDir, string toDir)
        {
            if (!Directory.Exists(fromDir))
                return;

            CopyDir(fromDir, toDir);
            Directory.Delete(fromDir, true);
        }
    }
}

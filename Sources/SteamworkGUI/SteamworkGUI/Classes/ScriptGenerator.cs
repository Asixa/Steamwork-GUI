using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SteamworkGUI
{
    class ScriptGenerator
    {
        static FileStream w_fs;
        static FileStream r_fs;
        static StreamWriter sw;
        static StreamReader sr;

        public static string Generator(int Appid,String game_path)
        {
            string str = Directory.GetCurrentDirectory();
            string filePath = str + @"\Scripts\app_build_[" + Appid + "].vpf";
            r_fs = new FileStream(str + @"\Scripts\Template\app_build_[].vpf", FileMode.Open);
            w_fs = new FileStream(filePath, FileMode.CreateNew);
            sw = new StreamWriter(w_fs);
            sr = new StreamReader(r_fs);
            string s1 = sr.ReadToEnd();
            s1 = s1.Replace("[Appid]", Appid.ToString());
            s1 = s1.Replace("[Appid1]", (Appid + 1).ToString());
            sw.WriteLine(s1);
            sw.Close();
            sr.Close();
            w_fs.Close();
            r_fs.Close();

            filePath = str + @"\Scripts\depot_build_[" + Appid + "].vpf";
            r_fs = new FileStream(str + @"\Scripts\Template\depot_build_[].vdf", FileMode.Open);
            w_fs = new FileStream(filePath, FileMode.CreateNew);
            sw = new StreamWriter(w_fs);
            sr = new StreamReader(r_fs);
            string s2 = sr.ReadToEnd();
            s2 = s2.Replace("[Appid]", Appid.ToString());
            s2 = s2.Replace("[P_Game_path]", Directory.GetParent(game_path).FullName);
            s2 = s2.Replace("[Game_path]", game_path + @"\*");
            sw.WriteLine(s2);
            sw.Close();
            sr.Close();
            w_fs.Close();
            r_fs.Close();
            return filePath;
            #region 奇怪的注释
            //string s1 = "{\n" +
            //    "	\"appid\"	\"233330\"\n" +
            //    "	\"desc\" \"Your build description here\"\n" +
            //    "	\"buildoutput\" \".." + "\"" + @"\output\" + "\"\n" +
            //    "	\"contentroot\" \".." + "\"" + @"\content\" + "\"\n" +
            //    "	\"setlive\"	\"\" \n" +
            //    "	\"preview\"	\"0\"\n" +
            //    "	\"local\"	\"\" \n" +
            //    "\n" +
            //    "	\"depots\"" +
            //    "{\n" +
            //    Appid + 1 + " depot_build_" + Appid + 1 + "1" + ".vdf\n" +
            //    "}\n" +
            //    "}";
            #endregion
        }
    }
}

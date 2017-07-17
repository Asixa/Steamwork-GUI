using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace SteamworkGUI
{
    class Save_Load
    {
        public static void SaveData()
        {
            System.Data.DataSet ds = new System.Data.DataSet("SteamworkGUI");
            System.Data.DataTable table = new System.Data.DataTable("Data");
            ds.Tables.Add(table);

            table.Columns.Add("account", typeof(string));
            table.Columns.Add("appid", typeof(int));
            table.Columns.Add("language", typeof(string));

            System.Data.DataRow row = table.NewRow();
            row[0] = MainWindow._instance.user_name;
            row[1] = MainWindow._instance.Appid;
            row[2] = MainWindow._instance.LanguagePack;

            ds.Tables["Data"].Rows.Add(row);
            string path = Environment.CurrentDirectory+"/data.xml";
            try
            {
                ds.WriteXml(path);
            }
            catch
            {

            }
        }

        public static void LoadData()
        {
            string filename= Environment.CurrentDirectory + "/data.xml";
            if (!File.Exists(filename)) return;
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(filename);
                XmlNode root = xmlDoc.SelectSingleNode("//Data");       
                if (root != null)
                {
                    MainWindow._instance.LanguagePack = root.SelectSingleNode("language").InnerText;
                    MainWindow._instance.user_name=MainWindow._instance.LoginWindow.account.Text= (root.SelectSingleNode("account")).InnerText;
                    MainWindow._instance.Appid =int.Parse((root.SelectSingleNode("appid")).InnerText);
                    MainWindow._instance.AppidInput.Value = MainWindow._instance.Appid;
                   
                }
                else
                {

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

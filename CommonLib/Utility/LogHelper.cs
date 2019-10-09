using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonLib.Utility
{
    public class LogHelper
    {
        /// <summary>
        /// 记录文本信息
        /// </summary>
        /// <param name="strTxt"></param>
        public static void SaveLog(string strTxt,string folderName="ErrorLog",string fileName="Log")
        {
            DateTime dt = DateTime.Now;
            if (!Directory.Exists(Application.StartupPath + "\\"+ folderName) )//如果不存在就创建file文件夹 
            {
                Directory.CreateDirectory(Application.StartupPath + "\\"+ folderName);
            }
            string strLogFileName = Application.StartupPath + "\\"+ folderName+"\\"+ fileName + "(" + dt.Year + "-" + dt.Month + "-" + dt.Day + ").txt";
            try
            {
                StreamWriter sw = new StreamWriter(strLogFileName, true);

                sw.WriteLine("\n" + dt.ToString()+ ":");
                sw.WriteLine(strTxt);
                sw.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}

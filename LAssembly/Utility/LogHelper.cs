using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MR.LTools.Utility
{
    public class LogHelper
    {
        public static void WriteLog( string logName, string content, string sPath ="")
        {
            if (string.IsNullOrEmpty(sPath))
            {
                sPath = Application.StartupPath;
            }
            else
                sPath += "\\";
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }
            using (StreamWriter sw = File.AppendText(sPath + logName + ".txt"))
            {
                sw.WriteLine( content);
                sw.Close();
            }
        }
    }
}

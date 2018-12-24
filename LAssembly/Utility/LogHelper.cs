using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LAssembly.Utility
{
    public class LogHelper
    {
        public static void WriteLoG(string sPath,string logName, string content)
        {
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

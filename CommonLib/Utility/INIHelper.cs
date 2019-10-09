using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace CommonLib.Utility
{
    public static class INIHelper
    {
        #region API函数声明

        [DllImport("kernel32")]//返回0表示失败，非0为成功
        private static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);

        [DllImport("kernel32")]//返回取得字符串缓冲区的长度
        private static extern long GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);


        #endregion

        #region 读Ini文件

        /// <summary>
        ///  读Ini文件
        /// </summary>
        /// <param name="Section">参数1(section):写入ini文件的某个小节名称（不区分大小写）。</param>
        /// <param name="Key">参数2(key):上面section下某个项的键名(不区分大小写)。</param>
        /// <param name="NoText">参数3(val):上面key对应的value</param>
        /// <param name="iniFilePath">参数4(filePath):ini的文件名，包括其路径(example: "c:\config.ini"</param>
        /// <returns></returns>
        public static string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(Section, Key, NoText, temp, 1024, iniFilePath);
                return temp.ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        #endregion

        #region 写Ini文件
        /// <summary>
        /// 写Ini文件
        /// </summary>
        /// <param name="Section">参数1(section):写入ini文件的某个小节名称（不区分大小写）。</param>
        /// <param name="Key">参数2(key):上面section下某个项的键名(不区分大小写)。</param>
        /// <param name="NoText">参数3(val):上面key对应的value</param>
        /// <param name="iniFilePath">参数4(filePath):ini的文件名，包括其路径(example: "c:\config.ini"</param>
        /// <returns></returns>
        public static bool WriteIniData(string Section, string Key, string Value, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                long OpStation = WritePrivateProfileString(Section, Key, Value, iniFilePath);
                if (OpStation == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}

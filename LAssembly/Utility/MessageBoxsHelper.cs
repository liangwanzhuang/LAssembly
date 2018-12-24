using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LAssembly.Utility
{
    public class MessageBoxsHelper
    {
        public static void ShowInformation(string inStr)
        {
            MessageBox.Show(inStr, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public static void ShowWarning(Form inForm, string inStr)
        {
            MessageBox.Show(inForm, inStr, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static void ShowWarning(string inStr)
        {
            MessageBox.Show(inStr, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static void ShowError(string inStr)
        {
            MessageBox.Show(inStr, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        public static bool ShowQuestion(string inStr)
        {
            return MessageBox.Show(inStr, "系统提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public static bool ShowYesNo(string inStr)
        {
            return MessageBox.Show(inStr, "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}

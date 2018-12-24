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

        /// <summary>
        /// 显示信息提示框。
        /// </summary>
        /// <param name="text">提示文本。</param>
        /// <returns>OK按钮。</returns>
        public static DialogResult ShowInfo(string text)
        {
            if (Application.OpenForms.Count == 0)
            {
                return MessageBox.Show(text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                return MessageBox.Show(Application.OpenForms[0], text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 显示询问提示框。
        /// </summary>
        /// <param name="text">提示文本。</param>
        /// <returns>Yes/No按钮。</returns>
        public static DialogResult ShowQuestion(string text)
        {
            if (Application.OpenForms.Count == 0)
            {
                return MessageBox.Show(text, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
            {
                return MessageBox.Show(Application.OpenForms[0], text, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }

        }

        /// <summary>
        /// 显示询问提示框。
        /// </summary>
        /// <param name="text">提示文本。</param>
        /// <returns>Yes/No/Cancel按钮。</returns>
        public static DialogResult ShowQuestionWithCancel(string text)
        {
            if (Application.OpenForms.Count == 0)
            {
                return MessageBox.Show(text, Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            }
            else
            {
                return MessageBox.Show(Application.OpenForms[0], text, Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            }
        }
    }
}

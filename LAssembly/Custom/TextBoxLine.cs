using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MR.LTools.Custom
{
    public partial class TextBoxLine :  System.Windows.Forms.TextBox
    {
        public TextBoxLine()
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Multiline = true;
            this.Size = new Size(244, 24);
        }
        /// <summary>
        /// 画线
        /// </summary>
        const int WM_PAINT = 0xF;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT)
            {
                using (Graphics g = this.CreateGraphics())
                {
                    using (Pen p = new Pen(Color.Black))
                    {
                        g.DrawLine(p, 0, this.Height - 1, this.Width, this.Height - 1);
                    }
                }
            }
        }
    }
}

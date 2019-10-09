using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PublicControl.Forms
{
    public partial class UCMessageBox : Form
    {
       
        private int count = 0;//用于停留
        private UCMessageBox()
        {
            //防止闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            InitializeComponent();
            this.Opacity = 0.7;
        }
        public void SetContent(string text)
        {
            lblContent.Text = text;
        }
        /// <summary>
        /// 展示内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="control"></param>
        public static void Show(string text,Control control)
        {
            UCMessageBox mbx = new UCMessageBox();
            Point screenPoint = control.PointToScreen(control.Location);
            int x = screenPoint.X/2;
            int y = screenPoint.Y-170;
            mbx.Location = new Point(x, y);
            mbx.SetContent(text);
            mbx.Show();
        }
        /// <summary>
        /// 关闭的时钟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            //停留0.5秒
            if (++count<5)
            {
                return;
            }
            //开始关闭-向上移动逐渐隐藏
            this.Opacity -= 0.07;//透明度
            this.Top -= 5;
            if (this.Opacity <= 0)
            {
                this.Close();
            }
        }
    }
}

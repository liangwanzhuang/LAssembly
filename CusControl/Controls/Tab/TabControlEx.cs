using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace CusControl
{
    public partial class TabControlEx :  System.Windows.Forms.TabControl
    {
        public Image _tabPageBackImage;
        public Image TabPageBackImage
        {
            get { return _tabPageBackImage; }
            set
            {
                _tabPageBackImage = value;
            }
        }

        public ImageList _tabPageImageList;
        public ImageList TabPageImageList
        {
            get { return _tabPageImageList; }
            set
            {
                _tabPageImageList = value;
            }
        }
        public TabControlEx()
        {
            InitializeComponent();
            base.SetStyle(
ControlStyles.UserPaint |                      // 控件将自行绘制，而不是通过操作系统来绘制  
ControlStyles.OptimizedDoubleBuffer |          // 该控件首先在缓冲区中绘制，而不是直接绘制到屏幕上，这样可以减少闪烁  
ControlStyles.AllPaintingInWmPaint |           // 控件将忽略 WM_ERASEBKGND 窗口消息以减少闪烁  
ControlStyles.ResizeRedraw |                   // 在调整控件大小时重绘控件  
ControlStyles.SupportsTransparentBackColor,    // 控件接受 alpha 组件小于 255 的 BackColor 以模拟透明  
true);                                         // 设置以上值为 true  
            base.UpdateStyles();
            this.SizeMode = TabSizeMode.Fixed;  // 大小模式为固定  
            this.ItemSize = new Size(44, 45);   // 设定每个标签的尺寸 
            if (TabPageBackImage == null)
            {
                TabPageBackImage = Properties.Resources._1562205559;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {

            for (int i = 0; i < this.TabCount; i++)
            {
                //e.Graphics.DrawRectangle(Pens.Red, this.GetTabRect(i));
                //e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias; 

                if (this.SelectedIndex == i)
                {
                    e.Graphics.DrawImage(TabPageBackImage, this.GetTabRect(i));
                }
                // （略）  

                // Calculate text position  
                Rectangle bounds = this.GetTabRect(i);
                PointF textPoint = new PointF();
                SizeF textSize = TextRenderer.MeasureText(this.TabPages[i].Text, this.Font);

                // 注意要加上每个标签的左偏移量X  
                textPoint.X
                    = bounds.X + (bounds.Width - textSize.Width) / 2;
                textPoint.Y
                    = bounds.Bottom - textSize.Height - this.Padding.Y;

                // Draw highlights  
                e.Graphics.DrawString(
                    this.TabPages[i].Text,
                    this.Font,
                    SystemBrushes.ControlLightLight,    // 高光颜色  
                    textPoint.X,
                    textPoint.Y);

                // 绘制正常文字  
                textPoint.Y--;
                e.Graphics.DrawString(
                    this.TabPages[i].Text,
                    this.Font,
                    SystemBrushes.ControlText,    // 正常颜色  
                    textPoint.X,
                    textPoint.Y);

                // 绘制图标  
                if (this.TabPageImageList != null)
                {
                    int index = this.TabPages[i].ImageIndex;
                    string key = this.TabPages[i].ImageKey;
                    Image icon = new Bitmap(25, 25);

                    if (index > -1)
                    {
                        icon = this.TabPageImageList.Images[index];
                    }
                    if (!string.IsNullOrEmpty(key))
                    {
                        icon = this.TabPageImageList.Images[key];
                    }
                    e.Graphics.DrawImage(
                        icon,
                        bounds.X + (bounds.Width - icon.Width) / 2,
                        bounds.Top + this.Padding.Y);
                }

            }
        }
        //// 对父窗体的引用  
        //Form oldman;
        //protected override void OnParentChanged(EventArgs e)
        //{
        //    // 如果没有劫持到，则搜索  
        //    if (oldman == null)
        //        oldman = this.FindForm();
        //    var ss = oldman.Text;
        //    MessageBox.Show(oldman.Text);
        //    oldman.Text = this.TabPages[0].Text;
        //}
       
    }
}

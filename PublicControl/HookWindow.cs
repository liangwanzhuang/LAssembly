using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using PublicControl.Properties;

namespace PublicControl
{
    internal class HookWindow : System.Windows.Forms.NativeWindow, System.IDisposable
    {
        private const int WM_NCCALCSIZE = 131;

        private const int WM_NCPAINT = 133;

        private const int WM_NCACTIVATE = 134;

        private const int WM_NCMOUSEMOVE = 160;

        private const int WM_NCLBUTTONDOWN = 161;

        private const int WM_NCLBUTTONDBLCLK = 163;

        private const int WM_SIZE = 5;

        private const int WM_PAINT = 15;

        private const int WM_WINDOWPOSCHANGING = 70;

        private System.Windows.Forms.Form mainForm;

        private bool isSkin = false;

        private int borderSysWidth;

        private int captionSysHeight;

        private int borderWidth = 8;

        private int captionHeight = 20;

        private System.Drawing.Rectangle top;

        private System.Drawing.Rectangle left;

        private System.Drawing.Rectangle bottom;

        private System.Drawing.Rectangle right;

        private System.Drawing.Rectangle title;

        private object sync = new object();

        private int w;

        private int h;

        private System.Drawing.SolidBrush cBrush;

        private System.Drawing.SolidBrush bBrush;

        private System.Random rand = new System.Random();

        private System.Collections.Generic.Dictionary<string, System.Drawing.Rectangle> CaptionButtonRectangles;

        private System.Collections.Generic.Dictionary<string, System.Drawing.Bitmap> CaptionButtonBMPs;

        public int BorderWidth
        {
            get
            {
                return this.borderWidth;
            }
            set
            {
                if (value > 40 || value < 0)
                {
                    this.borderWidth = 8;
                }
                else
                {
                    this.borderWidth = value;
                }
            }
        }

        public int CaptionHeight
        {
            get
            {
                return this.captionHeight;
            }
            set
            {
                if (value > 50 || value < 10)
                {
                    this.captionHeight = 20;
                }
                else
                {
                    this.captionHeight = value;
                }
            }
        }

        public bool IsSkin
        {
            get
            {
                return this.isSkin;
            }
            set
            {
                lock (this.sync)
                {
                    this.isSkin = value;
                }
            }
        }

        private void SetCaptionButtonRectangles()
        {
            if (this.CaptionButtonRectangles == null)
            {
                this.CaptionButtonRectangles = new System.Collections.Generic.Dictionary<string, System.Drawing.Rectangle>();
            }
            else
            {
                this.CaptionButtonRectangles.Clear();
            }
            int num = 12;
            int num2 = 12;
            int width = this.mainForm.Bounds.Width;
            int y = this.borderSysWidth + (this.borderWidth - this.borderSysWidth) + (this.title.Height - num2) / 2;
            System.Drawing.Rectangle value = new System.Drawing.Rectangle(width - num - 10 - this.borderWidth, y, num, num2);
            System.Drawing.Rectangle value2 = new System.Drawing.Rectangle(value.X - num - 10, y, num, num2);
            System.Drawing.Rectangle value3 = new System.Drawing.Rectangle(value2.X - num - 10, y, num, num2);
            this.CaptionButtonRectangles.Add("Close", value);
            this.CaptionButtonRectangles.Add("Max", value2);
            this.CaptionButtonRectangles.Add("Min", value3);
            this.CaptionButtonRectangles.Add("Normal", value2);
            this.CaptionButtonRectangles.Add("CloseEnter", value);
            this.CaptionButtonRectangles.Add("MaxEnter", value2);
            this.CaptionButtonRectangles.Add("MinEnter", value3);
            this.CaptionButtonRectangles.Add("NormalEnter", value2);
        }

        public void GetSkinData(string skinFile)
        {
            System.IO.Stream stream = null;
            try
            {
                SkinData skinData;
                if (skinFile != "")
                {
                    stream = new System.IO.FileStream(skinFile, System.IO.FileMode.Open);
                    System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    skinData = (SkinData)formatter.Deserialize(stream);
                }
                else
                {
                    skinData = default(SkinData);
                    skinData.Min = Resources.最小化;
                    skinData.MinF = Resources.最小化;
                    skinData.Max = Resources.最大化;
                    skinData.MaxF = Resources.最大化;
                    skinData.Close = Resources.关闭__1_;
                    skinData.CloseF = Resources.关闭__1_;
                    skinData.Normal = Resources.最大化;
                    skinData.NormalF = Resources.最大化;
                    skinData.logo = Resources.logo;
                    skinData.CaptionColor = Color.FromArgb(41, 178, 216); ;
                    skinData.FrameColor = Color.FromArgb(0, 151, 193); ;
                    skinData.FrameWidth = 2;
                    skinData.CaptionHeight = System.Windows.Forms.SystemInformation.CaptionHeight;
                }
                this.CaptionButtonBMPs = new System.Collections.Generic.Dictionary<string, System.Drawing.Bitmap>();
                this.CaptionButtonBMPs.Add("Close", skinData.Close);
                this.CaptionButtonBMPs.Add("CloseEnter", skinData.CloseF);
                this.CaptionButtonBMPs.Add("Max", skinData.Max);
                this.CaptionButtonBMPs.Add("MaxEnter", skinData.MaxF);
                this.CaptionButtonBMPs.Add("Min", skinData.Min);
                this.CaptionButtonBMPs.Add("MinEnter", skinData.MinF);
                this.CaptionButtonBMPs.Add("Normal", skinData.Normal);
                this.CaptionButtonBMPs.Add("NormalEnter", skinData.NormalF);
                this.CaptionButtonBMPs.Add("Logo", skinData.logo);
                this.CaptionHeight = skinData.CaptionHeight;
                this.BorderWidth = skinData.FrameWidth;
                this.cBrush = new System.Drawing.SolidBrush(skinData.CaptionColor);
                this.bBrush = new System.Drawing.SolidBrush(skinData.FrameColor);
                this.borderSysWidth = (this.mainForm.Bounds.Width - this.mainForm.ClientRectangle.Width) / 2;
                this.captionSysHeight = System.Windows.Forms.SystemInformation.CaptionHeight;
                this.borderWidth = skinData.FrameWidth;
                this.captionHeight = skinData.CaptionHeight;
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + "\r\n程序将退出！");
                System.Windows.Forms.Application.Exit();
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        private void mainForm_TextChanged(object sender, System.EventArgs e)
        {
            this.mainForm.Invalidate();
            this.mainForm.Refresh();
            System.IntPtr handle = this.mainForm.Handle;
            WinAPIs.ReleaseCapture();
            WinAPIs.SendMessage(handle, 133, 0, 0);
        }

        public HookWindow(System.Windows.Forms.Form mainForm, string skinfile)
        {
            this.mainForm = mainForm;
            this.mainForm.TextChanged += new System.EventHandler(this.mainForm_TextChanged);
            if (this.mainForm.Text == "")
            {
                this.mainForm.Text = "弹出窗体";
            }
            base.AssignHandle(mainForm.Handle);
            mainForm.HandleDestroyed += new System.EventHandler(this.mainForm_HandleDestroyed);
            this.borderSysWidth = (mainForm.Width - mainForm.ClientSize.Width) / 2;
            this.captionSysHeight = System.Windows.Forms.SystemInformation.CaptionHeight;
            this.GetSkinData(skinfile);
            this.w = this.borderSysWidth + this.borderWidth - this.borderSysWidth;
            this.h = this.captionSysHeight + this.captionHeight - this.captionSysHeight;
            int num = this.borderWidth - this.borderSysWidth;
            int num2 = this.captionHeight - this.captionSysHeight;
            this.mainForm.ClientSize = new System.Drawing.Size(mainForm.ClientSize.Width + 2 * num, mainForm.ClientSize.Height + 2 * num + num2);
            this.mainForm.MinimumSize = mainForm.Bounds.Size;
            this.GetRect();
        }

        private void mainForm_HandleDestroyed(object sender, System.EventArgs e)
        {
            this.ReleaseHandle();
        }

        private void DrawTitleButton(string ButtonName)
        {
            this.SetCaptionButtonRectangles();
            if (this.GetIsVisible(ButtonName))
            {
                System.IntPtr windowDC = WinAPIs.GetWindowDC(this.mainForm.Handle);
                System.Drawing.Graphics graphics = System.Drawing.Graphics.FromHdc(windowDC);
                graphics.DrawImage(this.CaptionButtonBMPs[ButtonName], this.CaptionButtonRectangles[ButtonName]);
                graphics.Dispose();
                WinAPIs.ReleaseDC(this.mainForm.Handle, windowDC);
            }
        }

        private void GetRect()
        {
            this.title = new System.Drawing.Rectangle(this.w, this.w, this.mainForm.Bounds.Width - 2 * this.w, this.h);
            this.top = new System.Drawing.Rectangle(0, 0, this.mainForm.Bounds.Width, this.w);
            this.left = new System.Drawing.Rectangle(0, 0, this.w, this.mainForm.Bounds.Height);
            this.bottom = new System.Drawing.Rectangle(0, this.mainForm.Bounds.Height - this.w, this.mainForm.Bounds.Width, this.w);
            this.right = new System.Drawing.Rectangle(this.mainForm.Bounds.Width - this.w, 0, this.w, this.mainForm.Bounds.Height);
        }

        private void DrawTitle()
        {
            System.IntPtr windowDC = WinAPIs.GetWindowDC(this.mainForm.Handle);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromHdc(windowDC);
            this.GetRect();
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(this.title.Width, this.title.Height);
            System.Drawing.Graphics graphics2 = System.Drawing.Graphics.FromImage(bitmap);
            graphics2.Clear(this.cBrush.Color);
            System.Drawing.Bitmap bitmap2 = new System.Drawing.Bitmap(this.CaptionButtonBMPs["Logo"], this.title.Height - 2, this.title.Height - 2);
            graphics2.DrawImage(bitmap2, 2, 1);
            if (!string.IsNullOrEmpty(this.mainForm.Text))
            {
                float height = graphics.MeasureString(this.mainForm.Text, System.Drawing.SystemFonts.CaptionFont).Height;
                float y = ((float)this.title.Height - height) / 2f;
                graphics2.DrawString(this.mainForm.Text, System.Drawing.SystemFonts.CaptionFont, System.Drawing.Brushes.White, (float)(bitmap2.Width + 5), y);
            }
            graphics.DrawImage(bitmap, this.title);
            graphics.FillRectangles(this.bBrush, new System.Drawing.Rectangle[]
			{
				this.top,
				this.left,
				this.bottom,
				this.right
			});
            graphics2.Dispose();
            bitmap.Dispose();
            bitmap2.Dispose();
            graphics.Dispose();
            WinAPIs.ReleaseDC(base.Handle, windowDC);
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (!this.isSkin)
            {
                base.WndProc(ref m);
            }
            else
            {
                int msg = m.Msg;
                switch (msg)
                {
                    case 131:
                        {
                            System.Drawing.Rectangle rectangle = (System.Drawing.Rectangle)m.GetLParam(typeof(System.Drawing.Rectangle));
                            int x = rectangle.Left + (this.borderWidth - this.borderSysWidth);
                            int y = rectangle.Top + (this.borderWidth - this.borderSysWidth) + (this.captionHeight - this.captionSysHeight);
                            int width = rectangle.Width - (this.borderWidth - this.borderSysWidth);
                            int height = rectangle.Height - (this.borderWidth - this.borderSysWidth);
                            rectangle = new System.Drawing.Rectangle(x, y, width, height);
                            System.Runtime.InteropServices.Marshal.StructureToPtr(rectangle, m.LParam, true);
                            break;
                        }
                    case 132:
                        break;
                    case 133:
                        this.DrawTitle();
                        this.DrawTitleButton("Min");
                        this.DrawTitleButton(this.GetMaxName(false));
                        this.DrawTitleButton("Close");
                        return;
                    case 134:
                        base.WndProc(ref m);
                        this.DrawTitle();
                        this.DrawTitleButton("Min");
                        this.DrawTitleButton(this.GetMaxName(false));
                        this.DrawTitleButton("Close");
                        return;
                    default:
                        switch (msg)
                        {
                            case 160:
                                {
                                    System.Drawing.Point pt = new System.Drawing.Point((int)m.LParam);
                                    pt.Offset(-this.mainForm.Left, -this.mainForm.Top);
                                    if (!this.CaptionButtonRectangles["Min"].Contains(pt) && !this.CaptionButtonRectangles["Max"].Contains(pt) && !this.CaptionButtonRectangles["Close"].Contains(pt))
                                    {
                                        this.DrawTitleButton("Min");
                                        this.DrawTitleButton(this.GetMaxName(false));
                                        this.DrawTitleButton("Close");
                                    }
                                    else if (this.CaptionButtonRectangles["Min"].Contains(pt))
                                    {
                                        this.DrawTitleButton("MinEnter");
                                    }
                                    else if (this.CaptionButtonRectangles["Max"].Contains(pt))
                                    {
                                        this.DrawTitleButton(this.GetMaxName(true));
                                    }
                                    else if (this.CaptionButtonRectangles["Close"].Contains(pt))
                                    {
                                        this.DrawTitleButton("CloseEnter");
                                    }
                                    break;
                                }
                            case 161:
                                {
                                    System.Drawing.Point pt2 = new System.Drawing.Point((int)m.LParam);
                                    pt2.Offset(-this.mainForm.Left, -this.mainForm.Top);
                                    if (this.CaptionButtonRectangles["Min"].Contains(pt2))
                                    {
                                        this.mainForm.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                                    }
                                    else if (this.CaptionButtonRectangles["Max"].Contains(pt2))
                                    {
                                        if (this.mainForm.WindowState == System.Windows.Forms.FormWindowState.Normal)
                                        {
                                            this.mainForm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                                        }
                                        else
                                        {
                                            this.mainForm.WindowState = System.Windows.Forms.FormWindowState.Normal;
                                        }
                                    }
                                    else if (this.CaptionButtonRectangles["Close"].Contains(pt2))
                                    {
                                        this.mainForm.Close();
                                    }
                                    if (this.mainForm.WindowState == System.Windows.Forms.FormWindowState.Maximized)
                                    {
                                        return;
                                    }
                                    break;
                                }
                        }
                        break;
                }
                base.WndProc(ref m);
            }
        }

        private bool GetIsVisible(string buttonName)
        {
            return this.mainForm.MaximizeBox || this.mainForm.MinimizeBox || (buttonName == "CloseEnter" || buttonName == "Close");
        }

        private string GetMaxName(bool isEnter = false)
        {
            string result;
            if (!isEnter)
            {
                if (this.mainForm.WindowState == System.Windows.Forms.FormWindowState.Normal)
                {
                    result = "Max";
                }
                else
                {
                    result = "Normal";
                }
            }
            else if (this.mainForm.WindowState == System.Windows.Forms.FormWindowState.Normal)
            {
                result = "MaxEnter";
            }
            else
            {
                result = "NormalEnter";
            }
            return result;
        }

        public void Dispose()
        {
            this.cBrush.Dispose();
            this.bBrush.Dispose();
            foreach (System.Collections.Generic.KeyValuePair<string, System.Drawing.Bitmap> current in this.CaptionButtonBMPs)
            {
                current.Value.Dispose();
            }
            this.CaptionButtonBMPs.Clear();
            this.CaptionButtonBMPs = null;
            this.CaptionButtonRectangles.Clear();
            this.CaptionButtonRectangles = null;
        }
    }
}

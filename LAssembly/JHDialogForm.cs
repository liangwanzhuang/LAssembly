using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using System.ComponentModel;
using MR.LTools;

namespace MR.LTools 
{
    public class JHDialogForm : XtraForm
    {
        public bool IS_MIN = true;

        public bool IS_MAX = true;

        public bool IS_CLOSE = true;

        private IContainer components = null;

        private System.Windows.Forms.Timer timer1;

        public JHDialogForm()
        {
            this.InitializeComponent();
        }

        private void JHDialogForm_Load(object sender, System.EventArgs e)
        {
            if (this.Text == "")
            {
                this.Text = "弹出窗体";
            }
            this.SetCaptionSkin();
        }

        public void SetCaptionSkin()
        {
            new CaptionSkin(this)
            {
                SkinFile = ""
            }.InstallSkin();
        }

        private void JHDialogForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
        }

        private void JHDialogForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.timer1 = new System.Windows.Forms.Timer();
            base.SuspendLayout();
            this.timer1.Interval = 50;
            base.Appearance.BackColor = System.Drawing.Color.FromArgb(235, 236, 239);
            Appearance.Options.UseBackColor = true;
            base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(345, 200);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.Name = "JHDialogForm";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JHDialogForm";
            base.Load += new System.EventHandler(this.JHDialogForm_Load);
            base.ResumeLayout(false);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MR.LTools
{
    internal class CaptionSkin
    {
        private string skinfile;

        private HookWindow hook = null;

        private System.Windows.Forms.Form mainForm;

        private bool controlBox;

        public string SkinFile
        {
            get
            {
                return this.skinfile;
            }
            set
            {
                this.skinfile = value;
            }
        }

        public System.Windows.Forms.Form MainForm
        {
            get
            {
                return this.mainForm;
            }
            set
            {
                this.mainForm = value;
                this.controlBox = this.mainForm.ControlBox;
            }
        }

        public CaptionSkin()
        {
        }

        public CaptionSkin(System.Windows.Forms.Form mainForm)
            : this()
        {
            this.MainForm = mainForm;
        }

        public void InstallSkin()
        {
            if (this.hook == null)
            {
                this.hook = new HookWindow(this.mainForm, this.skinfile);
            }
            this.hook.IsSkin = true;
          // this.mainForm.ControlBox = false;//底部任务栏不显示窗体图标
        }

        public void UninstallSkin()
        {
            if (this.hook != null)
            {
                this.hook.IsSkin = false;
                this.mainForm.ControlBox = this.controlBox;
            }
        }
    }
}

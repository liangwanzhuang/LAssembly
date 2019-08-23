﻿// 版权所有  黄正辉  交流群：568015492   QQ：623128629
// 文件名称：FrmWaiting.cs
// 创建日期：2019-08-15 16:05:09
// 功能描述：FrmWaiting
// 项目地址：https://gitee.com/kwwwvagaa/net_winform_custom_control
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CusControl.Forms
{
    public partial class FrmWaiting : FrmBase
    {
        public string Msg { get { return label2.Text; } set { label2.Text = value; } }
        public FrmWaiting()
        {
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.label1.ImageIndex == this.imageList1.Images.Count - 1)
                this.label1.ImageIndex = 0;
            else
                this.label1.ImageIndex++;

        }

        private void FrmWaiting_VisibleChanged(object sender, EventArgs e)
        {
            //this.timer1.Enabled = this.Visible;
        }

        protected override void DoEsc()
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            base.Opacity = 1.0;
            this.timer2.Enabled = false;
        }

        public void ShowForm(int intSleep = 1)
        {
            base.Opacity = 0.0;
            if (intSleep <= 0)
            {
                intSleep = 1;
            }
            base.Show();
            this.timer2.Interval = intSleep;
            this.timer2.Enabled = true;
        }
    }
}

﻿// 版权所有  黄正辉  交流群：568015492   QQ：623128629
// 文件名称：FrmDialog.cs
// 创建日期：2019-08-15 16:04:36
// 功能描述：FrmDialog
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
    public partial class FrmDialog : FrmBase
    {
        bool blnEnterClose = true;
        private FrmDialog(
            string strMessage,
            string strTitle,
            bool blnShowCancel = false,
            bool blnShowClose = false,
            bool blnisEnterClose = true)
        {
            InitializeComponent();
            if (!string.IsNullOrWhiteSpace(strTitle))
                lblTitle.Text = strTitle;
            lblMsg.Text = strMessage;
            if (blnShowCancel)
            {
                this.tableLayoutPanel1.ColumnStyles[1].Width = 1;
                this.tableLayoutPanel1.ColumnStyles[2].Width = 50;
            }
            else
            {
                this.tableLayoutPanel1.ColumnStyles[1].Width = 0;
                this.tableLayoutPanel1.ColumnStyles[2].Width = 0;
            }
            //btnCancel.Visible = blnShowCancel;
            //ucSplitLine_V1.Visible = blnShowCancel;
            btnClose.Visible = blnShowClose;
            blnEnterClose = blnisEnterClose;
            //if (blnShowCancel)
            //{
            //    btnOK.BtnForeColor = Color.FromArgb(255, 85, 51);
            //}
        }

        #region 显示一个模式信息框
        /// <summary>
        /// 功能描述:显示一个模式信息框
        /// 作　　者:HZH
        /// 创建日期:2019-03-04 15:49:48
        /// 任务编号:POS
        /// </summary>
        /// <param name="owner">owner</param>
        /// <param name="strMessage">strMessage</param>
        /// <param name="strTitle">strTitle</param>
        /// <param name="blnShowCancel">blnShowCancel</param>
        /// <param name="isShowMaskDialog">isShowMaskDialog</param>
        /// <param name="blnShowClose">blnShowClose</param>
        /// <param name="isEnterClose">isEnterClose</param>
        /// <param name="deviationSize">大小偏移，当默认大小过大或过小时，可以进行调整（增量）</param>
        /// <returns>返回值</returns>
        public static DialogResult ShowDialog(
            IWin32Window owner,
            string strMessage,
            string strTitle = "提示",
            bool blnShowCancel = false,
            bool isShowMaskDialog = true,
            bool blnShowClose = false,
            bool blnIsEnterClose = true,
            Size? deviationSize = null)
        {
            DialogResult result = DialogResult.Cancel;
            if (owner == null || (owner is Control && (owner as Control).IsDisposed))
            {
                var frm = new FrmDialog(strMessage, strTitle, blnShowCancel, blnShowClose, blnIsEnterClose)
                {
                    StartPosition = FormStartPosition.CenterScreen,
                    IsShowMaskDialog = isShowMaskDialog,
                    TopMost = true
                };
                if (deviationSize != null)
                {
                    frm.Width += deviationSize.Value.Width;
                    frm.Height += deviationSize.Value.Height;
                }
                result = frm.ShowDialog();
            }
            else
            {
                if (owner is Control)
                {
                    owner = (owner as Control).FindForm();
                }
                var frm = new FrmDialog(strMessage, strTitle, blnShowCancel, blnShowClose, blnIsEnterClose)
                {
                    StartPosition = (owner != null) ? FormStartPosition.CenterParent : FormStartPosition.CenterScreen,
                    IsShowMaskDialog = isShowMaskDialog,
                    TopMost = true
                };
                if (deviationSize != null)
                {
                    frm.Width += deviationSize.Value.Width;
                    frm.Height += deviationSize.Value.Height;
                }
                result = frm.ShowDialog(owner);
            }
            return result;
        }
        #endregion

        private void btnOK_BtnClick(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_BtnClick(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnClose_MouseDown(object sender, MouseEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        protected override void DoEnter()
        {
            if (blnEnterClose)
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void FrmDialog_VisibleChanged(object sender, EventArgs e)
        {
            
        }
    }
}

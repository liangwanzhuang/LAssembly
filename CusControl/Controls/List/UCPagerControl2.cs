﻿// 版权所有  黄正辉  交流群：568015492   QQ：623128629
// 文件名称：UCPagerControl2.cs
// 创建日期：2019-08-15 16:00:37
// 功能描述：PageControl
// 项目地址：https://gitee.com/kwwwvagaa/net_winform_custom_control
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CusControl.Controls
{
    [ToolboxItem(true)]
    public partial class UCPagerControl2 : UCPagerControlBase
    {
        public UCPagerControl2()
        {
            InitializeComponent();
            txtPage.txtInput.KeyDown += txtInput_KeyDown;
        }

        void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnToPage_BtnClick(null, null);
                txtPage.InputText = "";
            }
        }

        public override event PageControlEventHandler ShowSourceChanged;

       
        public override List<object> DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                base.DataSource = value;
                if (base.DataSource == null)
                    base.DataSource = new List<object>();
                ResetPageCount();
            }
        }
       
        public override int PageSize
        {
            get
            {
                return base.PageSize;
            }
            set
            {
                base.PageSize = value;
                ResetPageCount();
            }
        }

        public override void FirstPage()
        {
            if (PageIndex == 1)
                return;
            PageIndex = 1;
            StartIndex = (PageIndex - 1) * PageSize;
            ReloadPage();
            var s = GetCurrentSource();
            if (ShowSourceChanged != null)
            {
                ShowSourceChanged(s);
            }
        }

        public override void PreviousPage()
        {
            if (PageIndex <= 1)
            {
                return;
            }
            PageIndex--;

            StartIndex = (PageIndex - 1) * PageSize;
            ReloadPage();
            var s = GetCurrentSource();
            if (ShowSourceChanged != null)
            {
                ShowSourceChanged(s);
            }
        }

        public override void NextPage()
        {
            if (PageIndex >= PageCount)
            {
                return;
            }
            PageIndex++;
            StartIndex = (PageIndex - 1) * PageSize;
            ReloadPage();
            var s = GetCurrentSource();
            if (ShowSourceChanged != null)
            {
                ShowSourceChanged(s);
            }
        }

        public override void EndPage()
        {
            if (PageIndex == PageCount)
                return;
            PageIndex = PageCount;
            StartIndex = (PageIndex - 1) * PageSize;
            ReloadPage();
            var s = GetCurrentSource();
            if (ShowSourceChanged != null)
            {
                ShowSourceChanged(s);
            }
        }

        private void ResetPageCount()
        {
            if (PageSize > 0)
            {
                PageCount = base.DataSource.Count / base.PageSize + (base.DataSource.Count % base.PageSize > 0 ? 1 : 0);
            }
            txtPage.MaxValue = PageCount;
            txtPage.MinValue = 1;
            ReloadPage();
        }

        private void ReloadPage()
        {
            try
            {
                ControlHelper.FreezeControl(tableLayoutPanel1, true);
                List<int> lst = new List<int>();

                if (PageCount <= 9)
                {
                    for (var i = 1; i <= PageCount; i++)
                    {
                        lst.Add(i);
                    }
                }
                else
                {
                    if (this.PageIndex <= 6)
                    {
                        for (var i = 1; i <= 7; i++)
                        {
                            lst.Add(i);
                        }
                        lst.Add(-1);
                        lst.Add(PageCount);
                    }
                    else if (this.PageIndex > PageCount - 6)
                    {
                        lst.Add(1);
                        lst.Add(-1);
                        for (var i = PageCount - 6; i <= PageCount; i++)
                        {
                            lst.Add(i);
                        }
                    }
                    else
                    {
                        lst.Add(1);
                        lst.Add(-1);
                        var begin = PageIndex - 2;
                        var end = PageIndex + 2;
                        if (end > PageCount)
                        {
                            end = PageCount;
                            begin = end - 4;
                            if (PageIndex - begin < 2)
                            {
                                begin = begin - 1;
                            }
                        }
                        else if (end + 1 == PageCount)
                        {
                            end = PageCount;
                        }
                        for (var i = begin; i <= end; i++)
                        {
                            lst.Add(i);
                        }
                        if (end != PageCount)
                        {
                            lst.Add(-1);
                            lst.Add(PageCount);
                        }
                    }
                }

                for (int i = 0; i < 9; i++)
                {
                    UCBtnExt c = (UCBtnExt)this.tableLayoutPanel1.Controls.Find("p" + (i + 1), false)[0];
                    if (i >= lst.Count)
                    {
                        c.Visible = false;
                    }
                    else
                    {
                        if (lst[i] == -1)
                        {
                            c.BtnText = "...";
                            c.Enabled = false;
                        }
                        else
                        {
                            c.BtnText = lst[i].ToString();
                            c.Enabled = true;
                        }
                        c.Visible = true;
                        if (lst[i] == PageIndex)
                        {
                            c.RectColor = Color.FromArgb(255, 77, 59);
                        }
                        else
                        {
                            c.RectColor = Color.FromArgb(223, 223, 223);
                        }
                    }
                }
                ShowBtn(PageIndex > 1, PageIndex < PageCount);
            }
            finally
            {
                ControlHelper.FreezeControl(tableLayoutPanel1, false);
            }
        }

        private void page_BtnClick(object sender, EventArgs e)
        {
            PageIndex = (sender as UCBtnExt).BtnText.ToInt();
            StartIndex = (PageIndex - 1) * PageSize;
            ReloadPage();
            var s = GetCurrentSource();

            if (ShowSourceChanged != null)
            {
                ShowSourceChanged(s);
            }
        }

        protected override void ShowBtn(bool blnLeftBtn, bool blnRightBtn)
        {
            btnFirst.Enabled = btnPrevious.Enabled = blnLeftBtn;
            btnNext.Enabled = btnEnd.Enabled = blnRightBtn;
        }

        private void btnFirst_BtnClick(object sender, EventArgs e)
        {
            FirstPage();
        }

        private void btnPrevious_BtnClick(object sender, EventArgs e)
        {
            PreviousPage();
        }

        private void btnNext_BtnClick(object sender, EventArgs e)
        {
            NextPage();
        }

        private void btnEnd_BtnClick(object sender, EventArgs e)
        {
            EndPage();
        }

        private void btnToPage_BtnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPage.InputText))
            {
                PageIndex = txtPage.InputText.ToInt();
                StartIndex = (PageIndex - 1) * PageSize;
                ReloadPage();
                var s = GetCurrentSource();
                if (ShowSourceChanged != null)
                {
                    ShowSourceChanged(s);
                }
            }
        }

    }
}

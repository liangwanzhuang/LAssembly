﻿namespace PublicControl.Controls
{
    partial class UCProcessLineExt
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ucProcessLine1 = new PublicControl.Controls.UCProcessLine();
            this.SuspendLayout();
            // 
            // ucProcessLine1
            // 
            this.ucProcessLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucProcessLine1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ucProcessLine1.Font = new System.Drawing.Font("Arial Unicode MS", 10F);
            this.ucProcessLine1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.ucProcessLine1.Location = new System.Drawing.Point(18, 33);
            this.ucProcessLine1.MaxValue = 100;
            this.ucProcessLine1.Name = "ucProcessLine1";
            this.ucProcessLine1.Size = new System.Drawing.Size(399, 16);
            this.ucProcessLine1.TabIndex = 0;
            this.ucProcessLine1.Text = "ucProcessLine1";
            this.ucProcessLine1.Value = 0;
            this.ucProcessLine1.ValueBGColor = System.Drawing.Color.White;
            this.ucProcessLine1.ValueColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(119)))), ((int)(((byte)(232)))));
            this.ucProcessLine1.ValueTextType = PublicControl.Controls.ValueTextType.None;
            // 
            // UCProcessLineExt
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.ucProcessLine1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(59)))));
            this.Name = "UCProcessLineExt";
            this.Size = new System.Drawing.Size(434, 50);
            this.ResumeLayout(false);

        }

        #endregion

        private UCProcessLine ucProcessLine1;
    }
}

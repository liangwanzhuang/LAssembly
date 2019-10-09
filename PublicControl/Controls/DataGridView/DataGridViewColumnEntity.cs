﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PublicControl.Controls
{
    public class DataGridViewColumnEntity
    {
        public string HeadText { get; set; }
        public int Width { get; set; }
        public System.Windows.Forms.SizeType WidthType { get; set; }
        public string DataField { get; set; }
        public Func<object, string> Format { get; set; }
    }
}

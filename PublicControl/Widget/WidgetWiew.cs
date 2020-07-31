using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PublicControl.Controls;

namespace PublicControl.Widget
{
    public partial class WidgetWiew : UserControl
    {
        public WidgetWiew()
        {
            InitializeComponent();
        }
        public virtual  void WidgetRefresh()
        {
        
        }
        public virtual bool IsClose()
        {
            return true;
        }
    }
}

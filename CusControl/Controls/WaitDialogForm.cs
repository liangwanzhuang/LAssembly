using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CusControl
{
   public static class WaitDialogForm
    {
        static Thread load = null;
        public static void ShowWaitdialogFrom(string strMess, Control control=null)
        {
            Thread load = new Thread((ThreadStart)delegate
            {
                frmLoading frmLoading = new frmLoading(control);
                frmLoading.SetText(strMess);
                if (control == null)
                {
                    frmLoading.StartPosition = FormStartPosition.CenterScreen;
                }
                frmLoading.ShowDialog();
            });
            load.Start();
        }
        public static void CloseWaitdialogFrom()
        {
            load.Abort();
        }
    }
}

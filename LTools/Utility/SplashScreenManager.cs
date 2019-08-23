using System;
using System.Windows.Forms;

namespace R.LTools.Utility
{
    /// <summary>
    /// 另起一个线程跑Loading SplashScreen窗口，由主进程执行耗时操作
    /// </summary>
    public class SplashScreenManager
    {
        //自定义传入窗口，异步显示
        private Form LoadingForm;

        /// <summary>
        /// 初始化SplashScreenManager，需要传入一个Form窗体对象
        /// </summary>
        /// <param name="ParentForm">The Parent Form of LoadingForm </param>
        /// <param name="loadControl">LoadingForm To Show</param>
        public SplashScreenManager(Form LoadingForm)
        {
            this.LoadingForm = LoadingForm;
        }

        private void ShowWaitForm()
        {
            LoadingForm.BringToFront();//放在前端显示
            LoadingForm.Activate(); //当前窗体是LoadingForm
            LoadingForm.ShowDialog();
        }

        /// <summary>
        /// 显示加载窗体
        /// </summary>
        public void ShowLoading()
        {
            MethodInvoker invoker = new MethodInvoker(ShowWaitForm);
            invoker.BeginInvoke(null, null);
            /*Console.WriteLine("等待Loading窗体实例化"); */
            while (!LoadingForm.IsHandleCreated) {}
            // 把显示窗体放到最前面
            LoadingForm.Invoke(new MethodInvoker(()=> {
                LoadingForm.BringToFront();//放在前端显示
                LoadingForm.Activate(); //当前窗体是LoadingForm
            }));

        }

        /// <summary>
        /// 关闭loading窗体
        /// </summary>
        public void CloseWaitForm()
        {
            int err_count = 0;
            try
            {
                LoadingForm.Invoke(new MethodInvoker(()=> {
                    LoadingForm.Close();
                }));
            }
            catch (Exception)
            {
                //防止未初始化,重复去close,直到OK
                bool isOK = false;
                err_count++;
                while (!isOK&&err_count<20)
                {
                    try
                    {
                        isOK = true;
                        LoadingForm.Invoke(new MethodInvoker(() => {
                            LoadingForm.Close();
                        }));
                    }
                    catch (Exception) { isOK = false; err_count++; }
                }
            }
            finally
            {

            }
        }
    }
}

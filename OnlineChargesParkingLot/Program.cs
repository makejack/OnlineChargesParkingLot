using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;

namespace OnlineChargesParkingLot
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            using (Login login = new Login())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    AdminInfo adminInfo = login.Tag as AdminInfo;
                    Application.Run(new Main(adminInfo));
                }
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            GetExceptionMessage(ex);
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            GetExceptionMessage(e.Exception);
        }

        static void GetExceptionMessage(Exception ex)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("****************************异常文本****************************");
                sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
                sb.AppendLine("【异常方法】：" + ex.TargetSite);
                sb.AppendLine("***************************************************************");
                WriteException(sb.ToString());
            }
            finally
            {
                MessageBox.Show("系统运行时发行未知错误！请重新启动系统。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        static void WriteException(string message)
        {
            string path = Application.StartupPath + @"\ErrorLog";
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            string fileName = path + $@"\Error_{DateTime.Now.ToString("yyyyMMdd")}.txt";
            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                sw.WriteLine(message);
                sw.Close();
            }
        }
    }
}

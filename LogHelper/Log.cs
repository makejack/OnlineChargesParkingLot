using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;

[assembly: XmlConfigurator(ConfigFile = "Log4.config", Watch = true)]
namespace LogHelper
{
    public class Log
    {
        private static ILog m_Log;

        static Log()
        {
            m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public static void Info(string message)
        {
            m_Log.Info(message);
        }

        public static void Info(string message, Exception ex)
        {
            m_Log.Info(message, ex);
        }

        public static void Error(string message)
        {
            m_Log.Error(message);
        }

        public static void Error(string message, Exception ex)
        {
            m_Log.Error(message, ex);
        }

        public static void Warning(string message)
        {
            m_Log.Warn(message);
        }

        public static void Warning(string message, Exception ex)
        {
            m_Log.Warn(message, ex);
        }

        public static void Fatal(string message)
        {
            m_Log.Fatal(message);
        }

        public static void Fatal(string message, Exception ex)
        {
            m_Log.Fatal(message, ex);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F21.BLOGGER
{
    public class Logger
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Info(string msg)
        {
            logger.Info(msg);
        }

        public static void Warn(string msg)
        {
            logger.Warn(msg);
        }

        public static void Warn(Exception ex, string msg)
        {
            logger.Warn(ex);
        }

        public static void Warn(Exception ex)
        {
            logger.Warn(ex);
        }
        
        public static void Error(string msg)
        {
            logger.Error(msg);
        }

        public static void Error(Exception ex, string msg)
        {
            logger.Error(ex, msg);
        }

        public static void Error(Exception ex)
        {
            logger.Error(ex);
        }
    }
}

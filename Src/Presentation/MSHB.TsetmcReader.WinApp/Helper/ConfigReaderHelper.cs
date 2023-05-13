using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.TsetmcReader.WinApp.Helper
{
    public class ConfigReaderHelper
    {
        public static string GetNotificationUrl()
        {
            return ConfigurationManager.AppSettings["NotificationUrl"].ToString();
        }

        public static string GetWebsiteUrl()
        {
            var url =ConfigurationManager.AppSettings["WebsiteUrl"].ToString();
            return url;
        }

        public static string GetStartTime()
        {
            return ConfigurationManager.AppSettings["StartTime"].ToString();
        }

        public static string GetEndTime()
        {
            return ConfigurationManager.AppSettings["EndTime"].ToString();
        }

        public static int GetThreadSleepTime()
        {
            if (int.TryParse(ConfigurationManager.AppSettings["ThreadSleepTime"].ToString(), out int ThreadSleepTime))
                return ThreadSleepTime;

            return 1000;
        }

        public static int GetChartChangesTime()
        {
            if (int.TryParse(ConfigurationManager.AppSettings["ChartChangesTime"].ToString(), out int ChartChangesTime))
                return ChartChangesTime;

            return 60;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog.Config;
using NLog.Targets;

namespace BddFrameworkVs.Utlities
{
    public class Logger
    {
        private static NLog.Logger _logger;

        public static NLog.Logger GetLogger()
        {
            if (_logger == null)
            {
                var config = new LoggingConfiguration();
                var fileTarget = new FileTarget("logfile")
                {
                    FileName = Path.Combine(ExtentManager.testReultsPath, "${basedir}/Logs/logfile_${shortdate}.log"),
                    Layout = "${longdate} ${level:uppercase=true} ${message} ${exception:format=toString}"
                };

                config.AddTarget(fileTarget);
                config.LoggingRules.Add(new LoggingRule("*", NLog.LogLevel.Info, fileTarget));
                NLog.LogManager.Configuration = config;
                _logger = NLog.LogManager.GetCurrentClassLogger();
            }

            return _logger;
        }
    }
}

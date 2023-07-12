using Castle.Facilities.Logging;

namespace Matoapp.Infrastructure.Castle.Logging.Console
{
    public static class LoggingFacilityExtensions
    {
        public static LoggingFacility UseConsoleLogging(this LoggingFacility loggingFacility)
        {
            return loggingFacility.LogUsing<ConsoleLoggerFactory>();
        }
    }
}
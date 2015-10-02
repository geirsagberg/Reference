using Serilog;

namespace Reference.Common.Extensions
{
    public static class LoggingExtensions
    {
        public static void LogDebug(this object obj, string messageTemplate, params object[] propertyValues)
        {
            GetLogger(obj).Debug(messageTemplate, propertyValues);
        }

        //TODO: LogError, LogWarn etc.

        private static ILogger GetLogger(object obj)
        {
            return Log.Logger.ForContext(obj.GetType());
        }
    }
}
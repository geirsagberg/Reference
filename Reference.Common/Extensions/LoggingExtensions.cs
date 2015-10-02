using Reference.Common.Contracts;
using Reference.Common.Utils;

namespace Reference.Common.Extensions
{
    public static class LoggingExtensions
    {
        public static void LogDebug(this object obj, string messageTemplate, params object[] propertyValues)
        {
            GetLogger(obj).Debug(messageTemplate, propertyValues);
        }

        //TODO: LogError, LogWarn etc.

        private static ILog GetLogger(object obj)
        {
            return new SeriLogger(obj.GetType());
        }
    }
}
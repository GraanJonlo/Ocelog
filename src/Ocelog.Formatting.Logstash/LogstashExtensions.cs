using System;
using System.Reactive.Linq;

namespace Ocelog.Formatting.Logstash
{
    public static class LogstashExtensions
    {
        public static IObservable<LogEvent> AddTagsToAdditionalFields(this IObservable<LogEvent> logEvents)
        {
            return logEvents.Do(logEvent => logEvent.AddField(new { tags = logEvent.Tags }));
        }

        public static IObservable<LogEvent> AddCallerInfoToAdditionalFields(this IObservable<LogEvent> logEvents)
        {
            return logEvents.Do(logEvent => logEvent.AddField(new { logEvent.CallerInfo }));
        }

        public static IObservable<LogEvent> AddLevelToAdditionalFields(this IObservable<LogEvent> logEvents)
        {
            return logEvents.Do(logEvent => logEvent.AddField(new { logEvent.Level }));
        }

        public static IObservable<LogEvent> AddLevelToTags(this IObservable<LogEvent> logEvents)
        {
            return logEvents.Do(logEvent =>
            {

                logEvent.AddTag(LogLevelString(logEvent.Level));
            });
        }

        private static string LogLevelString(LogLevel l)
        {
            string result;
            switch (l)
            {
                    case LogLevel.Error:
                        result = "Error";
                        break;
                    case LogLevel.Info:
                        result = "Info";
                        break;
                    case LogLevel.Warn:
                        result = "Warn";
                        break;
                    default:
                        result = "Unknown";
                        break;
            }

            return result;
        }
    }
}

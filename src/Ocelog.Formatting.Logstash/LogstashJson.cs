using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Ocelog.Formatting.Logstash
{
    public static class LogstashJson
    {
        public static ProcessedLogEvent Process(LogEvent logEvent)
        {
            var requiredFields = new Dictionary<string, object>
            {
                { "@version", 1 },
                { "@timestamp", logEvent.Timestamp.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture) }
            };

            var allFields = new object[] { requiredFields }
                .Concat(logEvent.AdditionalFields)
                .Concat(new[] { logEvent.Content });

            return new ProcessedLogEvent { Content = ObjectMerging.Flatten(allFields) };
        }
    }
}

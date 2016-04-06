namespace Microsoft.OpenPublishing.Build.Loggers.Interface
{
    using System;
    using System.ComponentModel;

    using Microsoft.OpenPublishing.Build.Common;

    using Newtonsoft.Json;

    public enum MessageSeverity
    {
        Error,
        Warning,
        Info,
        Verbose
    }

    public class LogItem
    {
        /// <summary>
        /// Message to log
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Project class that throw the exception
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// Processing file
        /// </summary>
        [JsonProperty("file")]
        public string File { get; set; }

        /// <summary>
        /// Line number in file
        /// </summary>
        [JsonProperty("line")]
        public int? Line { get; set; }

        /// <summary>
        /// Message Severity
        /// </summary>
        [JsonProperty("message_severity")]
        public MessageSeverity MessageSeverity { get; set; }

        /// <summary>
        /// Log time
        /// </summary>
        [JsonProperty("date_time")]
        public DateTime DateTime { get; set; }

        public LogItem()
        {
        }

        public LogItem(
            string message,
            string source,
            string file,
            MessageSeverity messageSeverity)
            : this(message, source, file, null, DateTime.UtcNow, messageSeverity)
        {
        }

        public LogItem(
            string message,
            string source,
            string file,
            int line,
            MessageSeverity messageSeverity)
            : this(message, source, file, line, DateTime.UtcNow, messageSeverity)
        {
        }

        [JsonConstructor]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public LogItem(
            string message,
            string source,
            string file,
            int? line,
            DateTime dateTime,
            MessageSeverity messageSeverity)
        {
            Message = message;
            Source = source;
            File = file;
            Line = line;
            DateTime = dateTime;
            MessageSeverity = messageSeverity;
        }
    }
}

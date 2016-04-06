namespace Microsoft.OpenPublishing.Build.Loggers.Interface
{
    using System;

    public interface ILogger : IDisposable
    {
        /// <summary>
        /// Log information
        /// </summary>
        /// <param name="logItem">Log item</param>
        void Log(LogItem logItem);
    }
}

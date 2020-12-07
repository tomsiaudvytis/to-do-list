using System;

namespace Logger
{
    public enum LogLevel
    {
        Warning,
        Error
    }

    public interface ILogger
    {
        void Log(string message, LogLevel level);
    }

    public class Logger : ILogger
    {
        public void Log(string message, LogLevel level)
        {
           // Fake.
           // logger should go there.
        }
    }
}

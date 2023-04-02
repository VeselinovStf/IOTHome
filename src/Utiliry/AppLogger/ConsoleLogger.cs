using Microsoft.Extensions.Logging;

namespace AppLogger
{
    public class ConsoleLogger<T> : IAppLogger<T>
    {
        private readonly ILogger<T> logger;

        public ConsoleLogger(ILoggerFactory loggerFactory)
        {
            this.logger = loggerFactory.CreateLogger<T>();
        }

        public void LogDebug(string message)
        {
            logger.LogDebug(message);
        }

        public void LogError(string message)
        {
            logger.LogError(message);
        }

        public void LogInformation(string message)
        {
            logger.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            logger.LogWarning(message);
        }
    }
}

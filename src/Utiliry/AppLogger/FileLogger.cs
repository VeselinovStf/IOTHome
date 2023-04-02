using AppLogger.Exceptions;
using Microsoft.Extensions.Logging;

namespace AppLogger
{
    public class FileLogger<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;
        private readonly string? _logFilePath;
        public FileLogger(ILoggerFactory loggerFactory)
        {
            this._logFilePath =
             string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("LOG_FILE_PATH"))
             ? throw new FileLoggerException("Environment Variable: LOG_FILE_PATH must be set!")
             : Environment.GetEnvironmentVariable("LOG_FILE_PATH");

            this._logger = loggerFactory.CreateLogger<T>();

            if (!File.Exists(this._logFilePath))
            {
                var directory = Path.GetDirectoryName(this._logFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (FileStream fs = File.Create(this._logFilePath)) ;
            }
        }

        public void LogDebug(string message)
        {
            var msg = $"[ DEBUG ][ {DateTime.Now} ] : {message}";
            AddLog(msg);
            this._logger.LogDebug(msg);
        }

        public void LogError(string message)
        {
            var msg = $"[ ERROR ][ {DateTime.Now} ] : {message}";
            AddLog(msg);
            this._logger.LogError(msg);
        }

        public void LogInformation(string message)
        {
            var msg = $"[ INFO ][ {DateTime.Now} ] : {message}";
            AddLog(msg);
            this._logger.LogInformation(msg);
        }

        public void LogWarning(string message)
        {
            var msg = $"[ WARNING ][ {DateTime.Now} ] : {message}";
            AddLog(msg);
            this._logger.LogWarning(msg);
        }

        private void AddLog(string message)
        {
            File.AppendAllText(this._logFilePath, $"{message}{Environment.NewLine}");
        }
    }
}

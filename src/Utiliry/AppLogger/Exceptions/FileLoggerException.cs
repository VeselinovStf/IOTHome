using System.Runtime.Serialization;

namespace AppLogger.Exceptions
{
    public class FileLoggerException : Exception
    {
        public FileLoggerException()
        {
        }

        public FileLoggerException(string? message) : base(message)
        {
        }

        public FileLoggerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected FileLoggerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

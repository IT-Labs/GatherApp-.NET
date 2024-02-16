namespace GatherApp.Services
{
    public interface ILoggingService
    {
        /// <summary>
        /// Logs a debug message.
        /// </summary>
        /// <param name="message">The debug message to log.</param>
        /// <param name="args">Optional format arguments for the message.</param>
        void LogDebug(string message, params object[] args);

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">The informational message to log.</param>
        /// <param name="args">Optional format arguments for the message.</param>
        void LogInformation(string message, params object[] args);

        /// <summary>
        /// Logs an error message with an optional exception and additional arguments.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        /// <param name="ex">The optional exception to log.</param>
        /// <param name="args">Optional format arguments for the message.</param>
        void LogError(string message, Exception ex, params object[] args);
    }
}

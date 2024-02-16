using Microsoft.Extensions.Configuration;
using Serilog;

namespace GatherApp.Services.Impl
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogger _logger;

        public LoggingService(IConfiguration configuration)
        {
            _logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public void LogDebug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.Information(message, args);
        }

        public void LogError(string message, Exception ex, params object[] args)
        {
            _logger.Error(ex, message, args);
        }
    }
}

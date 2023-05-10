
namespace BLEExample.Services.ErrorHandling
{
    /// <summary>
    /// Provide methods for logging exceptions to a repository
    /// </summary>
    public class ErrorReportingService : IErrorReportingService
    {
        public void ReportError(string message)
        {
            //send data to some backend repo that stores crash reports and warnings
        }

        public void ReportError(string message, Exception exception)
        {
            //send data to some backend repo that stores crash reports and warnings
        }

        public async Task ReportErrorAsync(string message)
        {
            Task.FromResult(0);
            //send data async to some backend repo that stores crash reports and warnings
        }

        public async Task ReportErrorAsync(string message, Exception exception)
        {
            //send data async to some backend repo that stores crash reports and warnings
            Task.FromResult(0);
        }
    }
}

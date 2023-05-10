namespace BLEExample.Services.ErrorHandling
{
    /// <summary>
    /// Defines error reporting service
    /// </summary>
    public interface IErrorReportingService
    {
        void ReportError(string message);
        void ReportError(string message, Exception exception);
        Task ReportErrorAsync(string message);
        Task ReportErrorAsync(string message, Exception exception);
    }
}

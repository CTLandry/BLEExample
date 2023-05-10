
namespace BLEExample.Services.Dialog
{
    /// <summary>
    /// Definition for the dialog service
    /// </summary>
    public interface IDialogService
    {
        Task ShowAlert(string message, string title, string buttonLabel);

        Task<bool> ShowAlert(string message, string title, string confirmButton, string denyButton);
    }
}

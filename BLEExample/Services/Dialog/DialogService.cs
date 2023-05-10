
namespace BLEExample.Services.Dialog
{
    /// <summary>
    /// Dialog service class implementation
    /// </summary>
    public class DialogService : IDialogService
    {
        /// <summary>
        /// Show the user a one button modal 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="buttonLabel"></param>
        /// <returns></returns>
        public Task ShowAlert(string message, string title, string buttonLabel)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, buttonLabel);
        }

        /// <summary>
        /// Show a prompt with boolean choices
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="confirmButtonLabel"></param>
        /// <param name="denyButtonLabel"></param>
        /// <returns></returns>
        public async Task<bool> ShowAlert(string message, string title, string confirmButtonLabel, string denyButtonLabel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, confirmButtonLabel, denyButtonLabel);
        }
    }
}

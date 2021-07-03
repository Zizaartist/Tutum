using MvvmHelpers;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Tutum.Interfaces;
using Tutum.StaticValues;
using Tutum.StaticValues.StringResources;
using Tutum.Views.User.Profile.ChangePassword;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tutum.ViewModels.ChangePassword
{
    public class PasswordViewModel : BaseViewModel
    {
        private string newPassword;
        public string NewPassword
        {
            get => newPassword;
            set { SetProperty(ref newPassword, value, onChanged: () => Device.BeginInvokeOnMainThread(SendSMSCommand.ChangeCanExecute)); }
        }

        private string passwordRepeated;
        public string PasswordRepeated
        {
            get => passwordRepeated;
            set { SetProperty(ref passwordRepeated, value, onChanged: () => Device.BeginInvokeOnMainThread(SendSMSCommand.ChangeCanExecute)); }
        }

        private bool AllFieldsValid => !string.IsNullOrEmpty(NewPassword) &&
                                        PasswordRepeated == NewPassword &&
                                        IsNotBusy;

        public Command SendSMSCommand { get; }

        private readonly IUserDataService userService;

        public PasswordViewModel()
        {
            userService = DependencyService.Get<IUserDataService>();

            SendSMSCommand = new Command(async () => await SendSMS().ContinueWith(task =>
            {
                IsBusy = false;
                Device.BeginInvokeOnMainThread(SendSMSCommand.ChangeCanExecute);

                if (task.IsFaulted)
                {
                    HandleError(task.Exception.InnerException);
                }
            }), () => AllFieldsValid);
        }

        private async Task SendSMS()
        {
            IsBusy = true;
            Device.BeginInvokeOnMainThread(SendSMSCommand.ChangeCanExecute);

            var phone = (await userService.GetData()).Phone;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await SecureStorage.GetAsync(StorageKeys.TOKEN));

            var response = await client.PostAsync($"{ApiStrings.HOST}{ApiStrings.AUTH_SMS_CHECK}?phone={phone}", null);
            response.EnsureSuccessStatusCode();

            await Shell.Current.GoToAsync($"{nameof(PasswordSmsForm)}?{nameof(SmsViewModel.NewPassword)}={NewPassword}");
        }

        private void HandleError(Exception e)
        {
            Shell.Current.DisplayAlert(AppResources.Alert_Error_Title, $"{AppResources.Alert_Error_UnhandledException} - {e}", "Ok");
        }
    }
}

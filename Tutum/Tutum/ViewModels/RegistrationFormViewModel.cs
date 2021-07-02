using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tutum.Models;
using Tutum.StaticValues;
using Tutum.Views.Registration;
using Xamarin.Forms;

namespace Tutum.ViewModels
{
    public class RegistrationFormViewModel : BaseViewModel
    {
        private string phone;
        public string Phone
        {
            get => phone;
            set { SetProperty(ref phone, value, onChanged: () => Device.BeginInvokeOnMainThread(SMSCommand.ChangeCanExecute)); }
        }

        private string name;
        public string Name
        {
            get => name;
            set { SetProperty(ref name, value, onChanged: () => Device.BeginInvokeOnMainThread(SMSCommand.ChangeCanExecute)); }
        }

        private string password;
        public string Password
        {
            get => password;
            set { SetProperty(ref password, value, onChanged: () => Device.BeginInvokeOnMainThread(SMSCommand.ChangeCanExecute)); }
        }

        private string repeatPassword;
        public string RepeatPassword
        {
            get => repeatPassword;
            set { SetProperty(ref repeatPassword, value, onChanged: () => Device.BeginInvokeOnMainThread(SMSCommand.ChangeCanExecute)); }
        }

        private bool AllFieldsValid { get => !string.IsNullOrEmpty(Phone) &&
                                            !string.IsNullOrEmpty(Name) &&
                                            !string.IsNullOrEmpty(Password) &&
                                            Password == RepeatPassword &&
                                            IsNotBusy; }

        public Command SMSCommand { get; } //Sends SMS, awaits until succesful response is delivered

        public RegistrationFormViewModel() 
        {
            SMSCommand = new Command(async () => await SendSMS().ContinueWith(task =>
            {
                IsBusy = false;
                Device.BeginInvokeOnMainThread(SMSCommand.ChangeCanExecute);
                if (task.IsFaulted)
                {
                    HandleException(task.Exception.InnerException);
                }
            }), () => AllFieldsValid);
        }

        private async Task SendSMS() 
        {
            IsBusy = true; 
            Device.BeginInvokeOnMainThread(SMSCommand.ChangeCanExecute);

            HttpClient client = new HttpClient();

            var response = await client.PostAsync($"{ApiStrings.HOST}{ApiStrings.AUTH_SMS_CHECK}?phone={Phone}&registrationCheck=True", null);
            if (response.IsSuccessStatusCode)
            {
                await Shell.Current.GoToAsync($"{nameof(RegistrationFinalizeForm)}?{nameof(RegistrationViewModel.Phone)}={Phone}&{nameof(RegistrationViewModel.Name)}={Name}&{nameof(RegistrationViewModel.Password)}={Password}");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<ErrorModel>(result);

                await Shell.Current.DisplayAlert("Error", error.errorText, "Ok");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Unhandled exception", "Ok");
            }
        }

        private void HandleException(Exception e)
        {
            Shell.Current.DisplayAlert("Error", $"Unhandled exception - {e}", "Ok");
        }
    }
}

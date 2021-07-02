using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tutum.Models;
using Tutum.StaticValues;
using Tutum.Views.User.Profile.ChangeNumber;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tutum.ViewModels.ChangeNumber
{
    public class NumberViewModel : BaseViewModel
    {
        private string newNumber;
        public string NewNumber
        {
            get => newNumber;
            set { SetProperty(ref newNumber, value, onChanged: () => Device.BeginInvokeOnMainThread(SendSMSCommand.ChangeCanExecute)); }
        }

        private bool IsValidNumber => Regex.Match(NewNumber ?? "", @"^((8|\+7|7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$").Success;

        public Command SendSMSCommand { get; }

        public NumberViewModel()
        {
            SendSMSCommand = new Command(async () => await SendSMS().ContinueWith(task =>
            {
                IsBusy = false;
                Device.BeginInvokeOnMainThread(SendSMSCommand.ChangeCanExecute);

                if (task.IsFaulted)
                {
                    HandleError(task.Exception.InnerException);
                }
            }), () => IsValidNumber && IsNotBusy);
        }

        private async Task SendSMS() 
        {
            IsBusy = true;
            Device.BeginInvokeOnMainThread(SendSMSCommand.ChangeCanExecute);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await SecureStorage.GetAsync(StorageKeys.TOKEN));

            var response = await client.PostAsync($"{ApiStrings.HOST}{ApiStrings.AUTH_SMS_CHECK}?phone={NewNumber}", null);
            response.EnsureSuccessStatusCode();

            await Shell.Current.GoToAsync($"{nameof(NumberSmsForm)}?{nameof(SmsViewModel.NewNumber)}={NewNumber}");
        }

        private void HandleError(Exception e)
        {
            Shell.Current.DisplayAlert("Error", $"UnhandledException - {e}", "Ok");
        }
    }
}

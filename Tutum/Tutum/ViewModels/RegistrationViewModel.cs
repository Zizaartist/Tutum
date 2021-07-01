using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tutum.Models;
using Tutum.StaticValues;
using Tutum.Views.User.Main;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tutum.ViewModels
{
    [QueryProperty(nameof(Phone), nameof(Phone))]
    [QueryProperty(nameof(Name), nameof(Name))]
    [QueryProperty(nameof(Password), nameof(Password))]
    public class RegistrationViewModel : BaseViewModel
    {
        private string code;
        public string Code
        {
            get => code;
            set { SetProperty(ref code, value, onChanged: () => Device.BeginInvokeOnMainThread(RegisterCommand.ChangeCanExecute)); }
        }

        private string phone;
        public string Phone { set { phone = value; } } 

        private string name;
        public string Name { set { name = value; } }

        private string password;
        public string Password { set { password = value; } }

        public Command RegisterCommand { get; } //Sends user data with sms code

        public RegistrationViewModel() 
        {
            RegisterCommand = new Command(async () => await Register().ContinueWith(task =>
            {
                IsBusy = false;
                Device.BeginInvokeOnMainThread(RegisterCommand.ChangeCanExecute);
                if (task.IsFaulted)
                {
                    HandleException(task.Exception.InnerException);
                }
            }), () => !string.IsNullOrEmpty(Code) && IsNotBusy);
        }

        private async Task Register() 
        {
            IsBusy = true;

            HttpClient client = new HttpClient();

            var newUserData = new User 
            {
                Phone = phone,
                Name = name,
                Password = password
            };

            var json = JsonConvert.SerializeObject(newUserData);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{ApiStrings.HOST}{ApiStrings.AUTH_REGISTER}?code={Code}", data);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<TokenModel>(result);

                await SecureStorage.SetAsync(StorageKeys.TOKEN, token.AccessToken);

                await Shell.Current.GoToAsync($"//{nameof(MainForm)}");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) //пока обрабатываем только проблемы с SMS кодом
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
            Shell.Current.DisplayAlert("Error", $"Exception - {e}", "Ok");
        }
    }
}

using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tutum.Interfaces;
using Tutum.Models;
using Tutum.StaticValues;
using Tutum.StaticValues.StringResources;
using Tutum.Views.User.Main;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tutum.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string phone;
        public string Phone
        {
            get => phone;
            set { SetProperty(ref phone, value, onChanged: () => Device.BeginInvokeOnMainThread(AuthorizeCommand.ChangeCanExecute)); }
        }

        private string password;
        public string Password
        {
            get => password;
            set { SetProperty(ref password, value, onChanged: () => Device.BeginInvokeOnMainThread(AuthorizeCommand.ChangeCanExecute)); }
        }

        private bool AllFieldsAreValid { get => !string.IsNullOrEmpty(Phone) &&
                                                !string.IsNullOrEmpty(Password) &&
                                                IsNotBusy; }

        public Command AuthorizeCommand { get; }

        public LoginViewModel()
        {
            Task.Run(() => CheckToken());

            AuthorizeCommand = new Command(async () => await Authorize()
                .ContinueWith(task => 
                {
                    IsBusy = false;
                    Device.BeginInvokeOnMainThread(AuthorizeCommand.ChangeCanExecute);
                    if (task.IsFaulted) 
                    {
                        HandleError(task.Exception.InnerException);
                    }
                }), () => AllFieldsAreValid);
        }

        //Блокируем управление, ждем проверки наличия токена, разблокируем. Если есть - переадресация.
        private async Task CheckToken() 
        {
            IsBusy = true;
            Device.BeginInvokeOnMainThread(AuthorizeCommand.ChangeCanExecute);

            var token = await SecureStorage.GetAsync(StorageKeys.TOKEN);

            if (token != null) 
            {
                await Shell.Current.GoToAsync($"//{nameof(MainForm)}");
            }

            IsBusy = false;
            Device.BeginInvokeOnMainThread(AuthorizeCommand.ChangeCanExecute);
        }

        private async Task Authorize() 
        {
            IsBusy = true; 
            Device.BeginInvokeOnMainThread(AuthorizeCommand.ChangeCanExecute);

            HttpClient client = new HttpClient();

            var creds = new UserCredentials 
            {
                Phone = Phone,
                Password = Password
            };

            var json = JsonConvert.SerializeObject(creds);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{ApiStrings.HOST}{ApiStrings.AUTH_LOGIN}", data);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<TokenModel>(result);

                await SecureStorage.SetAsync(StorageKeys.TOKEN, token.AccessToken);

                var userService = DependencyService.Get<IUserDataService>();
                userService.GetRemoteData(); //получаем новые данные пользователя асинхронно при каждой авторизации

                await Shell.Current.GoToAsync($"//{nameof(MainForm)}");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                await Shell.Current.DisplayAlert(AppResources.Alert_Error_Title, AppResources.R_LoginForm_Error_InvalidCredentials, "Ok");
            }
            else
            {
                await Shell.Current.DisplayAlert(AppResources.Alert_Error_Title, AppResources.Alert_Error_NetworkException, "Ok");
            }
        }

        public void HandleError(Exception e) 
        {
            Shell.Current.DisplayAlert(AppResources.Alert_Error_Title, $"{AppResources.Alert_Error_UnhandledException} - {e}", "Ok");
        }
    }
}

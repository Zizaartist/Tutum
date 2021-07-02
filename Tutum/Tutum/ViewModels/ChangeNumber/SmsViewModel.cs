﻿using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tutum.Interfaces;
using Tutum.Models;
using Tutum.Services;
using Tutum.StaticValues;
using Tutum.Views.User.Profile;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tutum.ViewModels.ChangeNumber
{
    [QueryProperty(nameof(NewNumber), nameof(NewNumber))]
    public class SmsViewModel : BaseViewModel
    {
        private string newNumber;
        public string NewNumber
        {
            get => newNumber;
            set { newNumber = value; }
        }

        private string code;
        public string Code
        {
            get => code;
            set { SetProperty(ref code, value, onChanged: () => Device.BeginInvokeOnMainThread(ChangeNumberCommand.ChangeCanExecute)); }
        }

        public Command ChangeNumberCommand { get; }

        public SmsViewModel() 
        {
            ChangeNumberCommand = new Command(async () => await ChangeNumber().ContinueWith(task =>
            {
                IsBusy = false;
                Device.BeginInvokeOnMainThread(ChangeNumberCommand.ChangeCanExecute);

                if (task.IsFaulted)
                {
                    HandleError(task.Exception.InnerException);
                }
            }), () => (Code?.Length ?? 0) == 4);
        }

        private async Task ChangeNumber()
        {
            IsBusy = true;
            Device.BeginInvokeOnMainThread(ChangeNumberCommand.ChangeCanExecute);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await SecureStorage.GetAsync(StorageKeys.TOKEN));

            var response = await client.PutAsync($"{ApiStrings.HOST}{ApiStrings.USERS_CHANGE_PHONE}?newNumber={NewNumber}&code={Code}", null);
            if (response.IsSuccessStatusCode)
            {
                var userService = DependencyService.Get<IUserDataService>();
                await userService.GetRemoteData();

                await Shell.Current.GoToAsync($"//{nameof(ProfileForm)}");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var result = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<ErrorModel>(result);

                await Shell.Current.DisplayAlert("Error", error.errorText, "Ok");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Ошибка при отправке номера", "Ok");
            }
        }

        private void HandleError(Exception e)
        {
            Shell.Current.DisplayAlert("Error", $"UnhandledException - {e}", "Ok");
        }
    }
}

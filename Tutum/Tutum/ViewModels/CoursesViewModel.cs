using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Tutum.Models;
using Tutum.StaticValues;
using Xamarin.Essentials;

namespace Tutum.ViewModels
{
    public class CoursesViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Course> Collection { get; } = new ObservableRangeCollection<Course>();

        public CoursesViewModel()
        {
            Task.Run(() => GetRemoteData().ContinueWith(task => 
            {
                if (task.IsFaulted)
                {
                    HandleError(task.Exception.InnerException);
                }
            }));
        }

        public async Task GetRemoteData()
        {
            Collection.Clear();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await SecureStorage.GetAsync(StorageKeys.TOKEN));

            var response = await client.GetAsync($"{ApiStrings.HOST}{ApiStrings.COURSE_CONTROLLER}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var tempList = JsonConvert.DeserializeObject<List<Course>>(result);

            Collection.AddRange(tempList);
        }

        public async Task GetCachedData()
        {

        }

        public void HandleError(Exception e)
        {
            App.Current.MainPage.DisplayAlert("Error", $"Exception - {e}", "Ok");
        }
    }
}

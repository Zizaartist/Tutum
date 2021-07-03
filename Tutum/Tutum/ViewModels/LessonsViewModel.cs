using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tutum.Models;
using Tutum.StaticValues;
using Tutum.StaticValues.StringResources;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tutum.ViewModels
{
    [QueryProperty(nameof(CourseId), nameof(CourseId))]
    public class LessonsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Lesson> Collection { get; } = new ObservableRangeCollection<Lesson>();

        private int courseId;
        public int CourseId 
        {
            get => courseId;
            set 
            {
                courseId = value;
                Task.Run(() => GetRemoteData().ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        HandleError(task.Exception.InnerException);
                    }
                }));
            }
        }

        public async Task GetRemoteData() 
        {
            Collection.Clear();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await SecureStorage.GetAsync(StorageKeys.TOKEN));

            var response = await client.GetAsync($"{ApiStrings.HOST}{ApiStrings.LESSON_BY_COURSE}{CourseId}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var tempList = JsonConvert.DeserializeObject<List<Lesson>>(result);

            Collection.AddRange(tempList);
        }

        public async Task GetCachedData() 
        {
        
        }

        public void HandleError(Exception e) 
        {
            App.Current.MainPage.DisplayAlert(AppResources.Alert_Error_Title, $"{AppResources.Alert_Error_UnhandledException} - {e}", "Ok");
        }
    }
}

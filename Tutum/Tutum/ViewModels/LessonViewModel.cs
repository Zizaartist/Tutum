using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Tutum.Models;
using Tutum.StaticValues;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tutum.ViewModels
{
    [QueryProperty(nameof(LessonId), nameof(LessonId))]
    public class LessonViewModel : BaseViewModel
    {
        private Lesson lesson;
        public Lesson Lesson
        {
            get => lesson;
            set { SetProperty(ref lesson, value); }
        }

        private int lessonId;
        public int LessonId 
        {
            get => lessonId;
            set 
            { 
                lessonId = value;
                Task.Run(() => GetLesson().ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        HandleError(task.Exception.InnerException);
                    }
                }));
            }
        }

        public async Task GetLesson()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await SecureStorage.GetAsync(StorageKeys.TOKEN));

            var response = await client.GetAsync($"{ApiStrings.HOST}{ApiStrings.LESSON_CONTROLLER}{LessonId}");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            Lesson = JsonConvert.DeserializeObject<Lesson>(result);
        }

        public void HandleError(Exception e)
        {
            App.Current.MainPage.DisplayAlert("Error", $"Exception - {e}", "Ok");
        }
    }
}

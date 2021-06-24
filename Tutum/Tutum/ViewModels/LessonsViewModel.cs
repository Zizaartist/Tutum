using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tutum.Models;
using Tutum.StaticValues;
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
                Task.Run(() => GetRemoteData());
            }
        }

        public async Task GetRemoteData() 
        {
            try
            {
                HttpClient client = new HttpClient();

                var response = await client.GetAsync($"{ApiStrings.HOST}{ApiStrings.LESSON_CONTROLLER}{CourseId}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                var tempList = JsonConvert.DeserializeObject<List<Lesson>>(result);

                Collection.AddRange(tempList);
            }
            catch (HttpRequestException e) 
            {
                HandleError(e);
            }
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

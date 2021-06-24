using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tutum.Models;
using Tutum.StaticValues;
using TutumAPI.Models;

namespace Tutum.ViewModels
{
    public class CoursesViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Course> Collection { get; } = new ObservableRangeCollection<Course>();

        public CoursesViewModel()
        {
            Task.Run(() => GetRemoteData());
        }

        public async Task GetRemoteData()
        {
            try
            {
                HttpClient client = new HttpClient();

                var response = await client.GetAsync($"{ApiStrings.HOST}{ApiStrings.COURSE_CONTROLLER}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                var tempList = JsonConvert.DeserializeObject<List<Course>>(result);

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

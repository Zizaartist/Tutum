using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Tutum.Interfaces;
using Tutum.Models;
using Tutum.StaticValues;
using Xamarin.Essentials;

namespace Tutum.Services
{
    public class UserDataService : IUserDataService
    {
        private User UserData { get; set; }

        public UserDataService() 
        {
        
        }

        /// <summary>
        /// Получает данные из переменной
        /// </summary>
        public async Task<User> GetData()
        {
            //Если данные отсутствуют - получить с API
            if (UserData == null) 
            {
                await GetRemoteData();
            }

            return UserData;
        }

        /// <summary>
        /// Получает данные из API
        /// </summary>
        public async Task<User> GetRemoteData()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await SecureStorage.GetAsync(StorageKeys.TOKEN));

                var response = await client.GetAsync($"{ApiStrings.HOST}{ApiStrings.USERS_CONTROLLER}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                UserData = JsonConvert.DeserializeObject<User>(result);
            }
            catch
            {
                UserData = null; //другого выхода не вижу
            }

            return UserData;
        }
    }
}

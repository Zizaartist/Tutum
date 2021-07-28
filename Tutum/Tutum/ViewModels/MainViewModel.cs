using MvvmHelpers;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Tutum.StaticValues;
using Xamarin.CommunityToolkit.Core;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tutum.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string CacheFilePath = "";
        private const string CacheFileName = "tutumTeaser.mp4";

        public bool SourceChanged { get; private set; } = false;

        private MediaSource source;
        public MediaSource Source
        {
            get => source;
            set { SetProperty(ref source, value); }
        }

        public Command InitializeSource { get; }

        public MainViewModel() 
        {
            CacheFilePath = Path.Combine(FileSystem.CacheDirectory, CacheFileName);

            InitializeSource = new Command(() =>
            {
                SourceChanged = false;

                //Если кэшированный файл присутствует - установить его в качестве источника
                if (File.Exists(CacheFilePath))
                {
                    Source = new FileMediaSource { File = CacheFilePath };
                }
                else
                {
                    Source = new UriMediaSource { Uri = new Uri(ApiStrings.TEASER_PATH) };
                    Task.Run(LoadFileToCache);
                }
            });

            InitializeSource.Execute(null);
        }
        
        //Загружает файл в кэш
        private async Task LoadFileToCache() 
        {
            var client = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };

            try
            {
                using (var response = await client.GetAsync(ApiStrings.TEASER_PATH))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var videoBytes = await response.Content.ReadAsByteArrayAsync(); //Видео меньше мегабайта, мне кажется говнокод можно допустить
                        File.WriteAllBytes(CacheFilePath, videoBytes);
                        SourceChanged = true;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}

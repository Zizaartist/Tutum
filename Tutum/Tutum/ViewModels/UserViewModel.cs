using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Tutum.Interfaces;
using Tutum.Models;
using Tutum.Services;
using Tutum.StaticValues.StringResources;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace Tutum.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        #region properties

        private readonly IUserDataService _service;

        private User user;
        public User User
        {
            get => user;
            set { SetProperty(ref user, value); }
        }

        private string selectedLanguage;
        public string SelectedLanguage
        {
            get => selectedLanguage;
            set
            {
                SetProperty(ref selectedLanguage, value, onChanged: () =>
                {
                    var cultureName = AvailableLanguages.StringToStandart[value];
                    AppResources.Culture = CultureInfo.GetCultureInfo(cultureName);
                });
            }
        }

        public Command GetUserDataCommand { get; }
        public Command InitializeCommand { get; }

        #endregion properties

        public UserViewModel()
        {
            _service = DependencyService.Get<IUserDataService>();

            GetUserDataCommand = new Command(async () => User = await _service?.GetData());
            InitializeCommand = new Command(async () => await _service?.GetRemoteData());

            var currentCultureName = LocalizationResourceManager.Current.CurrentCulture.Name;
            string languageString = null;
            try
            {
                languageString = AvailableLanguages.StandartToString[currentCultureName];
            }
            catch (ArgumentNullException)
            {
                languageString = AvailableLanguages.Languages[0];
            }
            catch (KeyNotFoundException)
            {
                languageString = AvailableLanguages.Languages[0];
            }
            selectedLanguage = languageString;
        }
    }
}

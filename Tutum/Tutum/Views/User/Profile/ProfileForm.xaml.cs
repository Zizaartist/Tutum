using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutum.StaticValues;
using Tutum.Views.Registration;
using Tutum.Views.User.Profile.ChangeNumber;
using Tutum.Views.User.Profile.ChangePassword;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutum.Views.User.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileForm : ContentPage
    {
        public ProfileForm()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //Триггерим получение данных пользователя из сервиса каждый раз, при появлении на экране
            ViewModel.GetUserDataCommand.Execute(null);
        }

        private async void ChangePass_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(PasswordForm));
        }

        private async void ChangePhone_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(NumberForm));
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            SecureStorage.Remove(StorageKeys.TOKEN);

            Shell.Current.GoToAsync($"///{nameof(LoginForm)}");
        }
    }
}
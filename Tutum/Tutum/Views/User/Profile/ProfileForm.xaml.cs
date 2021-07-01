using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutum.StaticValues;
using Tutum.Views.Registration;
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

            ViewModel.GetUserDataCommand.Execute(null);
        }

        private void ChangePass_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Tutum", "Sorry, Profile is not available in demo version", "Ok");
        }

        private void SaveChanges_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Tutum", "Sorry, Profile is not available in demo version", "Ok");
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            SecureStorage.Remove(StorageKeys.TOKEN);

            Shell.Current.GoToAsync($"///{nameof(LoginForm)}");
        }
    }
}
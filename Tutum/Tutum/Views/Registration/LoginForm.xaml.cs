using System;
using Tutum.Views.User.Main;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutum.Views.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginForm : ContentPage
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync($"//{nameof(MainForm)}");
        }

        private void Registration_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(RegistrationForm));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            DisplayAlert("Tutum", "Sorry, Profile is not available in demo version", "Ok");
        }
    }
}
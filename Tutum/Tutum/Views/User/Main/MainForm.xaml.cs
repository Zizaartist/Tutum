using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutum.Views.User.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainForm : ContentPage
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Buy_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.instagram.com/tutumfitness/"));
        }
    }
}
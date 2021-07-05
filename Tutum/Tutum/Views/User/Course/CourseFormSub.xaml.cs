using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutum.Interfaces;
using Tutum.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutum.Views.User.Course
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseFormSub : ContentPage
    {
        public CourseFormSub()
        {
            InitializeComponent();

            var urlSource = new UrlWebViewSource();
            urlSource.Url = System.IO.Path.Combine(DependencyService.Get<IBaseUrl>().Get(), "player-example.html");

            WebPlayer.Source = urlSource;
            //WebPlayer.EvaluateJavaScriptAsync
            //WebPlayer.
        }

        private void Next_Clicked(object sender, EventArgs e)
        {

        }
    }
}
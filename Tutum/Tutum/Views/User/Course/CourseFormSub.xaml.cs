using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutum.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutum.Views.User.Course
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseFormSub : ContentPage
    {
        public CourseFormSub( string name, string uri)
        {
            InitializeComponent();
            Title.Text = name;
            mediaElement.Source = uri;
        }

        private void Next_Clicked(object sender, EventArgs e)
        {

        }
    }
}
using System.Linq;
using Tutum.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutum.Views.User.Course
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseForm : ContentPage
    {
        public CourseForm()
        {
            InitializeComponent();
        }

        private void CourseCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Any())
            {
                CourseCollection.SelectedItem = null;
                if ((e.CurrentSelection.LastOrDefault() as Course).Premium == "Free")
                {
                    Navigation.PushAsync(new CourseFormDescription(e.CurrentSelection.LastOrDefault() as Course));
                }
                else
                {
                    DisplayAlert("Tutum", "Please, buy premium account", "Ok");
                }
            }
        }
    }
}
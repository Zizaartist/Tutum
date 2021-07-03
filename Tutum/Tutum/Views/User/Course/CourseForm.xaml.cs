using System.Linq;
using Tutum.StaticValues.StringResources;
using Tutum.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutum.Views.User.Course
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseForm : ContentPage
    {
        private readonly UserViewModel userVM;

        public CourseForm()
        {
            InitializeComponent();

            userVM = DependencyService.Get<UserViewModel>();
            userVM.GetUserDataCommand.Execute(null);
        }

        private async void CourseCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Any())
            {
                var selectedCourse = e.CurrentSelection.LastOrDefault() as Tutum.Models.Course;
                CourseCollection.SelectedItem = null;

                var isSubscribed = userVM.User?.HasSubscription ?? false;

                if (!(selectedCourse).IsPremiumOnly || isSubscribed)
                {
                    await Shell.Current.GoToAsync($"{nameof(CourseFormDescription)}?{nameof(LessonsViewModel.CourseId)}={selectedCourse.CourseId}");
                }
                else
                {
                    await DisplayAlert(AppResources.Alert_Warning_Title, AppResources.U_C_CourseForm_Warning_Premium, "Ok");
                }
            }
        }
    }
}
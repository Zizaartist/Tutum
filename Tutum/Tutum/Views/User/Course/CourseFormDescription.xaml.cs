using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutum.Models;
using Tutum.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutum.Views.User.Course
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseFormDescription : ContentPage
    {
        public CourseFormDescription()
        {
            InitializeComponent();
        }

        private void LessonImage_Clicked(object sender, EventArgs e)
        {
            ImageButton button = sender as ImageButton;
            var selectedLesson = button.BindingContext as Lesson;

            Shell.Current.GoToAsync($"{nameof(CourseFormSub)}?{nameof(LessonViewModel.LessonId)}={selectedLesson.LessonId}");
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Any())
            {
                var selectedLesson = e.CurrentSelection.LastOrDefault() as Lesson;
                LessonsCollection.SelectedItem = null;

                await Shell.Current.GoToAsync($"{nameof(CourseFormSub)}?{nameof(LessonViewModel.LessonId)}={selectedLesson.LessonId}");
            }
        }
    }
}
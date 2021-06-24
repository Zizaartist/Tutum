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
    public partial class CourseFormDescription : ContentPage
    {
        Coursee _course;
        public CourseFormDescription(Coursee course)
        {
            InitializeComponent();
            Wrapper.BindingContext = course;
            _course = course;
        }

        private void LessonOne_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CourseFormSub("Lesson 1", "ms-appx:///C1L1.mp4"));
        }

        private void LessonTwo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CourseFormSub("Lesson 2", "ms-appx:///C1L2.mp4"));
        }

        private void LessonThree_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CourseFormSub("Lesson 3", "ms-appx:///C1L3.mp4"));
        }

        private void LessonFour_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CourseFormSub("Lesson 4", "ms-appx:///C1L4.mp4"));
        }

        private void LessonFive_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CourseFormSub("Lesson 5", "ms-appx:///C1L5.mp4"));
        }

        private void LessonSix_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CourseFormSub("Lesson 6", "ms-appx:///C1L6.mp4"));
        }
    }
}
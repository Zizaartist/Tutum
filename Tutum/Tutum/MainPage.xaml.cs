using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutum.Views.Registration;
using Tutum.Views.User.Course;
using Xamarin.Forms;

namespace Tutum
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            Routing.RegisterRoute($"{nameof(LoginForm)}/{nameof(RegistrationForm)}", typeof(RegistrationForm));
            Routing.RegisterRoute($"{nameof(LoginForm)}/{nameof(RegistrationForm)}/{nameof(RegistrationFinalizeForm)}", typeof(RegistrationFinalizeForm));

            Routing.RegisterRoute($"{nameof(CourseForm)}/{nameof(CourseFormDescription)}", typeof(CourseFormDescription));
            Routing.RegisterRoute($"{nameof(CourseForm)}/{nameof(CourseFormDescription)}/{nameof(CourseFormSub)}", typeof(CourseFormSub));
        }
    }
}

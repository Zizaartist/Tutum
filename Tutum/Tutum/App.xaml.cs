﻿using System;
using Tutum.Interfaces;
using Tutum.Services;
using Tutum.ViewModels;
using Tutum.Views.Registration;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tutum
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.RegisterSingleton<IUserDataService>(new UserDataService());
            DependencyService.Register<UserViewModel>();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

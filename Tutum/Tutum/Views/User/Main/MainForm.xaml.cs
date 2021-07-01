﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutum.ViewModels;
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

            var userVM = DependencyService.Get<UserViewModel>();

            //Если авторизовались - подгрузить данные пользователя с API,
            //т.к. токен должен присутствовать к этому моменту
            Task.Run(() => userVM.InitializeCommand.Execute(null)); 
        }

        private void Buy_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.instagram.com/tutumfitness/"));
        }
    }
}
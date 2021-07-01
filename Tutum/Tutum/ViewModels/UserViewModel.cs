using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tutum.Interfaces;
using Tutum.Models;
using Tutum.Services;
using Xamarin.Forms;

namespace Tutum.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        #region properties

        private readonly IUserDataService _service;

        private User user;
        public User User
        {
            get => user;
            set { SetProperty(ref user, value); }
        }

        public Command GetUserDataCommand { get; }
        public Command InitializeCommand { get; }

        #endregion properties

        public UserViewModel()
        {
            _service = DependencyService.Get<IUserDataService>();

            GetUserDataCommand = new Command(async () => User = await _service?.GetData());
            InitializeCommand = new Command(async () => await _service?.GetRemoteData());
        }
    }
}

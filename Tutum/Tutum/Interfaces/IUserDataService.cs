using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tutum.Models;

namespace Tutum.Interfaces
{
    interface IUserDataService
    {
        Task<User> GetData();

        Task<User> GetRemoteData();
    }
}

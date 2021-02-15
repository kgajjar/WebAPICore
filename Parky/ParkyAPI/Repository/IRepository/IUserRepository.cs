using ParkyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        //Check if user exists
        bool IsUniqueUser(string username);
        //Authenticate User
        User Authenticate(string username, string password);
        //Sends us a user object once auth has passed
        User Register(string username, string password);
    }
}

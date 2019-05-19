using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface IUserService
    {
        User AddUser(User user);
        void RemoveUser(User user);
        User GetUser(string id);
        User GetUserByName(string name, string password);
        List<User> GetUsers();
        void ToggleAdmin(string id);
    }
}

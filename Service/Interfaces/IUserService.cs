using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface IUserService
    {
        void AddUser(User user);
        void RemoveUser(User user);
        void EditUser(User user);
        User GetUser(string id);
        User GetUserByName(string name, string password);
    }
}


using Data.Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Implements
{
    public class UserService : IUserService
    {
        private readonly MainContext _appContext;
        public UserService(MainContext ctx)
        {
            _appContext = ctx;
        }
        public void AddUser(User user)
        {
            _appContext.Users.Add(user);
            _appContext.SaveChanges();
        }

        public void EditUser(User user)
        {
            _appContext.Users.Update(user);
            _appContext.SaveChanges();
        }
        public User GetUserByName(string name, string password) => _appContext.Users.FirstOrDefault(x => x.Email == name && x.PasswordHash == password);

        
        public User GetUser(string id) => _appContext.Users.FirstOrDefault(x => x.ID == id);

        public void RemoveUser(User user)
        {
            _appContext.Users.Remove(user);
            _appContext.SaveChanges();
        }
    }
}

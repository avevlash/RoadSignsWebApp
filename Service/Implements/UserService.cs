
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
        public User AddUser(User user)
        {
            _appContext.Users.Add(user);
            _appContext.SaveChanges();
            return user;
        }

        public User GetUserByName(string name, string password) => _appContext.Users.FirstOrDefault(x => x.Email == name && x.PasswordHash == password);

        
        public User GetUser(string id) => _appContext.Users.FirstOrDefault(x => x.ID == id);

        public void RemoveUser(User user)
        {
            _appContext.Users.Remove(user);
            _appContext.SaveChanges();
        }

        public List<User> GetUsers() => _appContext.Users.ToList();

        public void ToggleAdmin(string id)
        {
            User user = _appContext.Users.Find(id);
            user.IsAdmin = !user.IsAdmin;
            _appContext.Users.Update(user);
            _appContext.SaveChanges();
        }
    }
}

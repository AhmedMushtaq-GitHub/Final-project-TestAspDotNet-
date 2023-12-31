﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAspDotNet.Data;
using TestAspDotNet.Model;

namespace TestAspDotNet.Repository.Implementation
{
    public class AccountRepository : IAccount
    {
        private readonly TestAspDotNetDbContext _db;
        public AccountRepository(TestAspDotNetDbContext db)
        {
          _db = db;
        }
        public User GetUserForLogin(string email, string password)
        {
            //return _db.Users.Where(x => (string.IsNullOrEmpty(email) || x.Name.ToLower() == email.ToLower()) && x.Password.Equals(password)).FirstOrDefault();
            return _db.Users.Where(x => x.EmailAddress.ToLower().Equals(email.ToLower()) && x.Password.Equals(password)).FirstOrDefault();
        }

        public User GetUserInfo(string accessToken)
        {
            return _db.Users.Where(x => x.AccessToken.Equals(accessToken)).FirstOrDefault();
        }

        public string Register(User user)
        {
            user.UserRoleId = 1003;
            user.IsConfirm = true;
            user.JoinedOn = DateTime.UtcNow.AddHours(5);
            user.AccessToken = Guid.NewGuid().ToString() + DateTime.UtcNow.Ticks;
            _db.Users.Add(user);
            _db.SaveChanges();
            
            return user.AccessToken + user.JoinedOn.Ticks.ToString();
           
        }
    }
}

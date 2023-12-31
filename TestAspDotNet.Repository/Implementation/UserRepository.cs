﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAspDotNet.Data;
using TestAspDotNet.Model;

namespace TestAspDotNet.Repository.Implementation
{
    public class UserRepository : IUser
    {
        private readonly TestAspDotNetDbContext _db;
        public UserRepository(TestAspDotNetDbContext db)
        {
            _db = db;
        }



        public UserRole GetRole(int id)
        {
            return _db.UserRoles.Where(x => x.Id == id).FirstOrDefault();
        }
        public void AddUpdateRole(UserRole userRole)
        {
            _db.UserRoles.Update(userRole);
            _db.SaveChanges();
        }
        public List<UserRole> GetRoles()
        {
            return _db.UserRoles.ToList();
        }

        public void DeleteRole(int id)
        {
            UserRole userRole = _db.UserRoles.Where(x => x.Id == id).FirstOrDefault();
            _db.Remove(userRole);
            _db.SaveChanges();
        }

        public void DeleteRole()
        {
            throw new NotImplementedException();
        }
        //---------userRoleList

       public List<UserRole> GetRolesList()
        {
            return _db.UserRoles.ToList();
        }

        //---------------user
        public List<User> GetUsers()
        {
            return _db.Users.Include(x => x.UserRole).ToList();
        }
        public User GetUser(int id)
        {
            return _db.Users.Where(x => x.Id == id).FirstOrDefault();
        }
        public void AddUpdateUser(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges();

        }
        public void DeleteUser(int id)
        {
            User user = _db.Users.Where(x => x.Id == id).FirstOrDefault();
            _db.Remove(user);
            _db.SaveChanges();
        }
    }
}

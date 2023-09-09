using System;
using System.Collections.Generic;
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
            return _db.Users.Where(x => x.EmailAddress.ToLower().Equals(email.ToLower()) && x.Password.Equals(password)).FirstOrDefault();
        }

        public string Registry(User user)
        {
            user.UserRoleId = 3;
            user.IsConfirm = false;
            user.JoinedOn = DateTime.UtcNow.AddHours(5);
            user.AccessToken = Guid.NewGuid().ToString() + DateTime.UtcNow.Ticks;

            _db.SaveChanges();
            _db.Users.Add(user);
            return user.AccessToken + user.JoinedOn.Ticks.ToString();
        }
    }
}

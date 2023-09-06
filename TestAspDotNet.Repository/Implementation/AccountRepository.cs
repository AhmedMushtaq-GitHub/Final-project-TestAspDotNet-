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

        
    }
}

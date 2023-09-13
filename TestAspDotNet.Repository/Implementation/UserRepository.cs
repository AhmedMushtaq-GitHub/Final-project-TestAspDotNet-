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
        public List<UserRole> GetRoles()
        {
            return _db.UserRoles.ToList();
        }
    }
}

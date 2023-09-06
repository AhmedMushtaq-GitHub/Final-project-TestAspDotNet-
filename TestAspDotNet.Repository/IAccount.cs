using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TestAspDotNet.Model;

namespace TestAspDotNet.Repository
{
    public interface IAccount
    {
     /*   string Register(User user);
        string GetUserInfo();*/
        public User GetUserForLogin(string email, string password);
    }
}

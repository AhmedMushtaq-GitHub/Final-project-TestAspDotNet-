using Microsoft.Win32;
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
  
        public User GetUserForLogin(string email, string password);
        string Registry(User user);
        
    }
}

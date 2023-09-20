using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAspDotNet.Model;

namespace TestAspDotNet.Repository
{
    public interface IUser
    {
        List<UserRole> GetRoles();
        UserRole GetRole(int id);
        void AddUpdateRole(UserRole userRole);
        void DeleteRole();
        void DeleteRole(int id);
        //--------Users
        List<User> GetUsers();
        User GetUser(int id);
        void AddUpdateUser(User user);
        void DeleteUser(int id);
        List<UserRole> GetRolesList();
    }
}

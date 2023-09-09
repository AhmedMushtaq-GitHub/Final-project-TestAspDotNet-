using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAspDotNet.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccessToken { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public DateTime JoinedOn { get; set; }
        public bool IsConfirm { get; set; }
        public UserRole? UserRole { get; set; }  
        public virtual int UserRoleId { get; set; }  
    }
}

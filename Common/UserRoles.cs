
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Common
{
  
    public class UserRoles
    {
        //we have list so we need a constructor
        public UserRoles()
        {
            userRoles = new List<string>();
        }
        public IdentityUser user { get; set; }
        public List<string> userRoles { get; set; }
    }
}

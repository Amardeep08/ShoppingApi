using ShoppingAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingAPI.Security
{
    public class UserSecurity
    {
        public static bool Login(string username,string password)
        {
            using (angularShoppingEntities dbEntities = new angularShoppingEntities())
            {
                return dbEntities.tbl_user.Any(x => x.username.Equals( username,StringComparison.OrdinalIgnoreCase) && x.password==password);
               
            }
        }
    }
}
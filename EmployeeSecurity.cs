using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeDataAccess;

namespace NetFrameworkWebAPI
{
    public class EmployeeSecurity
    {
        public static bool Login(string username, string password)
        {
            using (DevDBEntities entities = new DevDBEntities())
            {
                return entities.Users.Any(user =>
                       user.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
                                          && user.Password == password);
            }
        }
    }
}